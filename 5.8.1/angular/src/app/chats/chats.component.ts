import { UserDto, UserServiceProxy, UserPerRelationServiceProxy, MessageServiceProxy, UserDtoPagedResultDto } from './../../shared/service-proxies/service-proxies';
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

    this._usersService.getAll
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
    abp.message.info('StartChat:'+user.name);
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
