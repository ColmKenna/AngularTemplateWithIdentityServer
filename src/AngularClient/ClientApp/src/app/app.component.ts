import { Component } from '@angular/core';
import { AuthService } from './auth/auth-service.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  isLoggedIn = false;

  constructor(private _authService: AuthService) {
    this._authService.loginChanged.subscribe(loggedIn => {
      this.isLoggedIn = loggedIn;
    })
  }


  ngOnInit() {
    this._authService.isLoggedIn().then(loggedIn => {
      this.isLoggedIn = loggedIn;
    })
  }

  login() {
    this._authService.login();
  }

  logout() {
    this._authService.logout();
  }

}
