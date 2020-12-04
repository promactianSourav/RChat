import { UserDto, UserServiceProxy, UserPerRelationServiceProxy, MessageServiceProxy, UserDtoPagedResultDto, GetMessageOutput, CreateMessageInput, CreateUserPerRelationInput, GetUserPerRelationOutput } from './../../shared/service-proxies/service-proxies';
import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';

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
  
  users: UserDto[] = [];
  messages:GetMessageOutput[] = [];
  getMsg:GetMessageOutput = new GetMessageOutput();
  userPerRelationForChatOne:CreateUserPerRelationInput = new CreateUserPerRelationInput();
  userPerRelationForChatTwo:CreateUserPerRelationInput = new CreateUserPerRelationInput();
  getUserPerRealtionForgettingMessagesOne:GetUserPerRelationOutput = new GetUserPerRelationOutput();
  getUserPerRealtionForgettingMessagesTwo:GetUserPerRelationOutput = new GetUserPerRelationOutput();
  messageInput:string = "";
  user:UserDto = new UserDto();
   cmsg:CreateMessageInput = new CreateMessageInput();
  senderId:number = null;
  receiverId:number = null;
  
  keyword = '';

  constructor(
    injector: Injector,
    private _usersService: UserServiceProxy,
    private _userPerRelationsService: UserPerRelationServiceProxy,
    private _messagesService: MessageServiceProxy,
    private _modalService: BsModalService
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
        this.showPaging(result, pageNumber);
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
    // abp.message.info('StartChat:'+user.name);
    this.senderId = abp.session.userId;
    this.receiverId = user.id;
    this.user = user;

    this.userPerRelationForChatOne.senderId = this.senderId;
    this.userPerRelationForChatOne.receiverId = this.receiverId;

    this.userPerRelationForChatTwo.senderId = this.receiverId;
    this.userPerRelationForChatTwo.receiverId = this.senderId;
    
    this._userPerRelationsService.create(this.userPerRelationForChatOne).subscribe((response) => {
      console.log(response);
      
    });
    this._userPerRelationsService.create(this.userPerRelationForChatTwo).subscribe((response) => {
      console.log(response);
      
    });
    this._userPerRelationsService.getUserPerRelationForSenderAndReceiver(this.senderId,this.receiverId).subscribe((response)=>{
      this.getUserPerRealtionForgettingMessagesOne = response;
    });
    this._userPerRelationsService.getUserPerRelationForSenderAndReceiver(this.receiverId,this.senderId).subscribe((response)=>{
      this.getUserPerRealtionForgettingMessagesTwo = response;
    })
    this._messagesService.getAllForBothUser(this.getUserPerRealtionForgettingMessagesOne.id,this.getUserPerRealtionForgettingMessagesTwo.id).subscribe((response)=>{
     
      response.forEach(element => {
        if(element.isRead==false){
          element.isRead = true;
        }
      });
      this.messages = response;
    });
    
  }

  sendMessage(){
    this.cmsg.messageContent = this.messageInput;
    this.cmsg.isRead = false;
    this._userPerRelationsService.getUserPerRelationForSenderAndReceiver(this.senderId,this.receiverId).subscribe((response) =>{
      this.cmsg.userPerRelationId = response.id;
    });

    this._messagesService.create(this.cmsg).subscribe((response)=>{
      this.getMsg.id = response.id;
      this.getMsg.messageContent = response.messageContent;
      this.getMsg.userPerRelationId = response.userPerRelationId;
      this.getMsg.isRead = response.isRead;
      this.messages.push(this.getMsg);
    })

    
  }

  onChangeInput(val:any){
    this.message = val;
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
