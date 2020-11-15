import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router'
import { Observable } from 'rxjs';
import {UserService} from './user.service';

@Injectable()
export class AuthGuard implements CanActivate{

  constructor(private userService: UserService, private router: Router){}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {

    const isAuth = this.userService.getAuthData();
    if(isAuth){
      this.router.navigate(["/home"]);
    }
    return true;
  }
}
