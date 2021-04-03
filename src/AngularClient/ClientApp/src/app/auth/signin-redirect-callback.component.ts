import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth-service.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signin-callback',
  template: `<div></div>`
})

export class SigninRedirectCallbackComponent implements OnInit {
  constructor(private _authService: AuthService,
              private _router: Router) { }

  addressFromState(stateData: string): string {
    let returnAddress = '/';

    if (stateData.search('returnUrl') >= 0) {
      
      try {
        let stateAsJson = JSON.parse(stateData);
        if (stateAsJson.hasOwnProperty('returnUrl')) {
          returnAddress = stateAsJson.returnUrl;
        }
      } catch (e) {
        returnAddress = "/";
      } 

    }
    return returnAddress;
    

    
  }
  ngOnInit() {


    this._authService.completeLogin().then(user => {
      let address = this.addressFromState(user.state);

      this._router.navigate([address]);
    }, rej => {
        this._router.navigate(['/unauthorized']);
        return rej;

      }
      )
  }
}
