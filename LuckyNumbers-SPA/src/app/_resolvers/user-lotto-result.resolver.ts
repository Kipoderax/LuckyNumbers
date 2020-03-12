import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { UserService } from '../_service/user.service';
import { AuthService } from '../_service/auth.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LottoResult } from '../_model/lottoResult';

@Injectable()
export class UserLottoResultResolver implements Resolve<LottoResult> {

    constructor(private userService: UserService,
                private router: Router,
                private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<LottoResult> {
        return this.userService.getLottoResult(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
