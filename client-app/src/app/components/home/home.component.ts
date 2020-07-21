import { Component, OnInit } from "@angular/core";

import { UserService } from "src/app/services/user.service";
import { ICustomer } from "src/app/models/customer";
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  customer: ICustomer;

  constructor(
    private userService: UserService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.authService.getClaims().then(
      claims => this.userService.fetchCustomerData(claims.email, claims.picture).subscribe(
        data => this.customer = data
      )
    );
  }

  logout() {
    this.authService.logout();
  }
}
