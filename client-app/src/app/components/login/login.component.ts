import { Component } from "@angular/core";

import { AuthService } from "angularx-social-login";
import { GoogleLoginProvider } from "angularx-social-login";
import { Router } from "@angular/router";
import { IUser } from "src/app/models/user";
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent {
  user: IUser;

  constructor(private authService: AuthService, private router: Router, private userService: UserService) { }

  signInWithGoogle(): void {
    this.authService
      .signIn(GoogleLoginProvider.PROVIDER_ID)
      .then(googleUser => {
        this.userService.googleLogin(googleUser).subscribe(
          userToken => {
            sessionStorage.setItem("access_token", userToken.accessToken);
            this.router.navigate(["home"]);
          }
        );
      })
      .catch((error) => {
        console.log(error);
      });
  }
}
