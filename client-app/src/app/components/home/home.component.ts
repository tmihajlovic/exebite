import { Component, OnInit } from "@angular/core";

import { Router } from "@angular/router";
import { UserService } from "src/app/services/user.service";
import { IUser } from "src/app/models/user";
import { ICustomer } from "src/app/models/customer";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  user: IUser;
  customers: ICustomer[];
  customer: ICustomer;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit() {
    this.userService.getUser().subscribe((data) => {
      this.customer = data;
      console.log(this.customer);
    });
  }

  signOutNavigate(): void {
    this.userService.signOut();
    this.router.navigate(["/"]);
  }
}
