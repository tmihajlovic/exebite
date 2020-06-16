import { Component } from "@angular/core";

import { AuthService } from "angularx-social-login";
import { GoogleLoginProvider } from "angularx-social-login";
import { Router } from "@angular/router";
import { IUser } from "src/app/models/user";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent {
  user: IUser;

  constructor(private authService: AuthService, private router: Router) {}

  signInWithGoogle(): void {
    this.authService
      .signIn(GoogleLoginProvider.PROVIDER_ID)
      .then(() => {
        this.router.navigate(["home/1"]);
      })
      .catch((error) => {
        console.log(error);
      });
  }
}
