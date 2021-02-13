import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '../account.service';

@Injectable()
export class ApproveGuard implements CanActivate{

  constructor(private userService: AccountService, private router: Router){}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    const isApproved = this.userService.isApproved;
      if(isApproved == false){
        return false;
      }
    return true;
  }
}
