import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router'
import { Observable } from 'rxjs';
import {AccountService} from '../account.service';

@Injectable()
export class AuthGuard implements CanActivate{

  constructor(private userService: AccountService, private router: Router){}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {

    const isAuth = this.userService.getAuthData();
    if(isAuth != null){
        return true;
    }
    return false;
  }
}
