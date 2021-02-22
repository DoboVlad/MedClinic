import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router'
import { Observable } from 'rxjs';
import {AccountService} from '../account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate{

  constructor(private userService: AccountService, private router: Router){}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    const isAuth = this.userService.getAuthData();
    console.log(isAuth);
    if(isAuth != null){
        console.log('In authguard' + isAuth);
        return true;
    }
    return false;
  }
}
