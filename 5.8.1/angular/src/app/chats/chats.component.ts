import { MessageSignal } from './../../shared/Models/MessageSignal';
import { ChatServiceService } from './../../shared/services/chat-service.service';
import { UserDto, UserServiceProxy, UserPerRelationServiceProxy, MessageServiceProxy, UserDtoPagedResultDto, GetMessageOutput, CreateMessageInput, CreateUserPerRelationInput, GetUserPerRelationOutput } from './../../shared/service-proxies/service-proxies';
import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalService } from 'ngx-bootstrap/modal';
import { finalize, map, mergeMap } from 'rxjs/operators';
import { forkJoin } from 'rxjs';

class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}
@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.css'
  ],
  animations: [appModuleAnimation()]
})
export class ChatsComponent extends PagedListingComponentBase<UserDto> {
  
  blank:boolean = false;
  users: UserDto[] = [];
  messages:GetMessageOutput[] = [];
  getMsg:GetMessageOutput;
  getSignalMsg:GetMessageOutput;
  userPerRelationForChatOne:CreateUserPerRelationInput = new CreateUserPerRelationInput();
  userPerRelationForChatTwo:CreateUserPerRelationInput = new CreateUserPerRelationInput();
  getUserPerRealtionForgettingMessagesOne:GetUserPerRelationOutput = new GetUserPerRelationOutput();
  getUserPerRealtionForgettingMessagesTwo:GetUserPerRelationOutput = new GetUserPerRelationOutput();
  messageInput:string = "";
  user:UserDto = new UserDto();
   cmsg:CreateMessageInput = new CreateMessageInput();
   cmsg2:CreateMessageInput;
   countReverseUserPerRelationId:number;
  senderId:number = abp.session.userId;
  receiverId:number = null;
  loggedInUserName:string = "";

  noti:string="";
  count:string="";
  notiUserId:number=0;
  messageOwner:string = "Me";
  whomYouChat:string = "";
  
  keyword = '';

  constructor(
    injector: Injector,
    private _usersService: UserServiceProxy,
    private _userPerRelationsService: UserPerRelationServiceProxy,
    private _messagesService: MessageServiceProxy,
    private _modalService: BsModalService,
    private _chatService: ChatServiceService
  ) {
    super(injector);
  }

  
  list(
    request: PagedUsersRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    request.isActive = true;

    
    this._usersService
      .getAll(request.keyword,request.isActive, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: UserDtoPagedResultDto) => {
        this.users = result.items;
        result.items.forEach(element => {
          if(element.id==abp.session.userId){
            this.loggedInUserName = element.name;
          }
        });
        this.showPaging(result, pageNumber);
      });



      this._chatService.signalReceived.subscribe((msg:MessageSignal)=>{
        // console.log("Your message arrived: "+msg);
        // console.log(msg.messageCurrentUserPerRelationId+"notcmsg");
        // console.log(msg.messageDescription);
        // console.log(msg.messageReceiverId);
        // console.log(msg.messageUnReadCount);
        // console.log(this.cmsg.userPerRelationId+"cmsg");
        
        
        
        if(this.cmsg.userPerRelationId==msg.messageCurrentUserPerRelationId){
          this.getSignalMsg = new GetMessageOutput();
          this.getSignalMsg.id = msg.messageId;
          this.getSignalMsg.userPerRelationId = this.countReverseUserPerRelationId;
          this.getSignalMsg.isRead = true;
          this.getSignalMsg.messageContent = msg.messageDescription;
          // console.log(msg.messageDescription+"notimessage");
          // this.messageOwner = this.whomYouChat;
          this.messages.push(this.getSignalMsg);
          // console.log(msg.messageId+" messageId");
          
          this._messagesService.updateSingleUnReadMessageToRead(msg.messageId).subscribe((response)=>{

          });
        }else{
          if(abp.session.userId==msg.messageReceiverId){
            this.notiUserId = msg.messageSenderId;
            this.count = msg.messageUnReadCount.toString();
          }
         
          // console.log(this.notiUserId);
          // console.log(this.count);
          
          
        }
        
        
      });
  }

  delete(user: UserDto): void {
    abp.message.confirm(
      this.l('RoleDeleteWarningMessage', user.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._usersService
            .delete(user.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('SuccessfullyDeleted'));
                this.refresh();
              })
            )
            .subscribe(() => {});
        }
      }
    );
  }


  startchat(user:UserDto){
    this.blank=true;
    // abp.message.info('StartChat:'+user.name);
    this.senderId = abp.session.userId;
    this.receiverId = user.id;
    this.user = user;

    this.userPerRelationForChatOne.senderId = this.senderId;
    this.userPerRelationForChatOne.receiverId = this.receiverId;

    this.userPerRelationForChatTwo.senderId = this.receiverId;
    this.userPerRelationForChatTwo.receiverId = this.senderId;
    this.whomYouChat = user.name;
    const first = this._userPerRelationsService.create(this.userPerRelationForChatOne).pipe(
      map(response =>{
        // console.log(response);
        return response;
        
      }),
        mergeMap(response => 
          this._userPerRelationsService.getUserPerRelationForSenderAndReceiver(response.senderId,response.receiverId))
      );

      const second = this._userPerRelationsService.create(this.userPerRelationForChatTwo).pipe(
        map(response =>{
          // console.log(response);
          return response;
          
        }),
          mergeMap(response => 
            this._userPerRelationsService.getUserPerRelationForSenderAndReceiver(response.senderId,response.receiverId))
        );

    


         forkJoin([first,second]).pipe(
          map(response =>{
            this.cmsg.userPerRelationId = response[0].id;
            this.countReverseUserPerRelationId = response[1].id;
            // console.log(response);
            return response;
          }),
            mergeMap(response => {
              const third = this._messagesService.updateUnReadMessageToRead(response[1].id);
              const fourth =this._messagesService.getAllForBothUser(response[0].id,response[1].id);
              
              return forkJoin([third,fourth]);
            })
          ).subscribe(res =>{
            // console.log(res);
            this.count = "";
            // res[1].forEach(element => {
            //   if(element.isRead==false && element.userPerRelationId == this.countReverseUserPerRelationId){
            //     element.isRead = true;
            //   }
            // });
            this.messages = res[1];
          });

         

    // this._userPerRelationsService.create(this.userPerRelationForChatOne).subscribe((response) => {
    //   console.log(response);
      
    // });
    // this._userPerRelationsService.create(this.userPerRelationForChatTwo).subscribe((response) => {
    //   console.log(response);
      
    // });
    // this._userPerRelationsService.getUserPerRelationForSenderAndReceiver(this.senderId,this.receiverId).subscribe((response)=>{
    //   this.getUserPerRealtionForgettingMessagesOne = response;
    //   console.log(response);
      
    // });
    // this._userPerRelationsService.getUserPerRelationForSenderAndReceiver(this.receiverId,this.senderId).subscribe((response)=>{
    //   this.getUserPerRealtionForgettingMessagesTwo = response;
    //   console.log(response);
      
      
    // })
    // this._messagesService.getAllForBothUser(this.getUserPerRealtionForgettingMessagesOne.id,this.getUserPerRealtionForgettingMessagesTwo.id).subscribe((response)=>{
    //   console.log(response);
      
    //   response.forEach(element => {
    //     if(element.isRead==false){
    //       element.isRead = true;
    //     }
    //   });
    //   this.messages = response;
    // });
    
  }

  sendMessage(){
    // console.log(this.messageInput+"msgINput");
    this.cmsg2 = new CreateMessageInput();
    this.cmsg2.messageContent = this.messageInput;
    this.cmsg2.isRead = false;
    this.cmsg2.userPerRelationId = this.cmsg.userPerRelationId;

    
    // this._userPerRelationsService.getUserPerRelationForSenderAndReceiver(this.senderId,this.receiverId).subscribe((response) =>{
    //   this.cmsg.userPerRelationId = response.id;
    // });
//     console.log(this.cmsg.userPerRelationId+" itsIdcsmg");
    
// console.log(this.cmsg.messageContent+" itsmessage");
this.getMsg = new GetMessageOutput();
this.getMsg.id = 0;
this.getMsg.messageContent = this.messageInput;
this.getMsg.userPerRelationId = this.cmsg2.userPerRelationId;
this.getMsg.isRead = true;
this.messages.push(this.getMsg);

    this._messagesService.create(this.cmsg2).subscribe((response)=>{
     
    })

    
  }

  onChangeInput(val:any){
    this.messageInput = val;
    // console.log(val+" val");
    
  }

  // createRole(): void {
  //   this.showCreateOrEditRoleDialog();
  // }

  // editRole(role: RoleDto): void {
  //   this.showCreateOrEditRoleDialog(role.id);
  // }

  // showCreateOrEditRoleDialog(id?: number): void {
  //   let createOrEditRoleDialog: BsModalRef;
  //   if (!id) {
  //     createOrEditRoleDialog = this._modalService.show(
  //       CreateRoleDialogComponent,
  //       {
  //         class: 'modal-lg',
  //       }
  //     );
  //   } else {
  //     createOrEditRoleDialog = this._modalService.show(
  //       EditRoleDialogComponent,
  //       {
  //         class: 'modal-lg',
  //         initialState: {
  //           id: id,
  //         },
  //       }
  //     );
  //   }

  //   createOrEditRoleDialog.content.onSave.subscribe(() => {
  //     this.refresh();
  //   });
  // }
}
