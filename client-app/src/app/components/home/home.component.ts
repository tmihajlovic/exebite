import { Component, OnInit } from "@angular/core";

import { AuthService } from "angularx-social-login";
import { Router } from "@angular/router";
import { UserService } from "src/app/user.service";
import { IUser } from "src/app/user";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  user: IUser;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.userService.getUserName().subscribe((data) => {
      this.user = data;
    });
  }

  signOut(): void {
    this.authService.signOut();
    this.router.navigate(["/"]);
  }
}
