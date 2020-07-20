import { Component, OnInit } from "@angular/core";

import { Router } from "@angular/router";
import { UserService } from "src/app/services/user.service";
import { IUser } from "src/app/models/user";
import { ICustomer } from "src/app/models/customer";
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  user: IUser;
  customers: ICustomer[];
  customer: ICustomer;

  constructor(private userService: UserService, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.userService.fetchCustomerData(this.authService.getClaims().email, this.authService.getClaims().picture).subscribe(
      data => {
        this.customer = data;
      }
    );
  }

  logout() {
    this.authService.logout();
  }
}
