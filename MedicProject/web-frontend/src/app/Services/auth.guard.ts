import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router'
import { Observable } from 'rxjs';
import {UserService} from './user.service';

@Injectable()
export class AuthGuard implements CanActivate{

  constructor(private userService: UserService, private router: Router){}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {

    const isAuth = this.userService.getAuthData();
    const isApproved = this.userService.isApproved;
    if(isAuth != null){
      console.log(isApproved);
      if(isApproved == false){
        this.router.navigate(["/waiting"]);
        return false;
      }
    }
    return true;
  }
}
