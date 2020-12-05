import { state } from '@angular/animations';
import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from 'shared/paged-listing-component-base';
import {
  UserServiceProxy,
  UserDto,
  UserDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateUserDialogComponent } from './create-user/create-user-dialog.component';
import { EditUserDialogComponent } from './edit-user/edit-user-dialog.component';
import { ResetPasswordDialogComponent } from './reset-password/reset-password.component';
import * as signalR from '@aspnet/signalr';
import { AppConsts } from '@shared/AppConsts';
class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './users.component.html',
  animations: [appModuleAnimation()]
})
export class UsersComponent extends PagedListingComponentBase<UserDto> {
  users: UserDto[] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  private hubConnection : signalR.HubConnection;
  createUser(): void {
  //   abp.signalr = {
  //     autoConnect: true,
  //     connect: undefined,
  //     hubs: undefined,
  //     qs: AppConsts.authorization.encryptedAuthTokenName + '=' + encodeURIComponent(encryptedAuthToken),
  //     remoteServiceBaseUrl: AppConsts.remoteServiceBaseUrl,
  //     startConnection: undefined,
  //     url: '/signalr'
  // };


    // this.hubConnection = new signalR.HubConnectionBuilder()
    // .withUrl(AppConsts.remoteServiceBaseUrl + "/signalr")
    // .build();

    
    // this.hubConnection.start()
    //   .then(() => console.log("Connected.")
    //   ).catch(err => console.log('Error while starting connection: '+err)
    //   );
    // console.log(AppConsts.remoteServiceBaseUrl + "/signalr");
    
    // console.log(this.hubConnection.state+"ok");
    // console.log(signalR.HubConnectionState.Disconnected+"ok2");
    
    
    // if(this.hubConnection.state === signalR.HubConnectionState.Connected){
    //   console.log("Hello ! Its connected.");
    //   this.hubConnection.stop();
      
    // }else{
    //   console.log("Oh! Its disconnected.");
    //   this.hubConnection.start()
    //   .then(() => console.log("Connected Again.")
    //   ).catch(err => console.log('Error while starting connection: '+err)
    //   );
      
    // }
    this.showCreateOrEditUserDialog();
  }

  editUser(user: UserDto): void {
    this.showCreateOrEditUserDialog(user.id);
  }

  public resetPassword(user: UserDto): void {
    this.showResetPasswordUserDialog(user.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedUsersRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    request.isActive = this.isActive;

    this._userService
      .getAll(
        request.keyword,
        request.isActive,
        request.skipCount,
        request.maxResultCount
      )
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

  protected delete(user: UserDto): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', user.fullName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._userService.delete(user.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showResetPasswordUserDialog(id?: number): void {
    this._modalService.show(ResetPasswordDialogComponent, {
      class: 'modal-lg',
      initialState: {
        id: id,
      },
    });
  }

  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    if (!id) {
      createOrEditUserDialog = this._modalService.show(
        CreateUserDialogComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditUserDialog = this._modalService.show(
        EditUserDialogComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
