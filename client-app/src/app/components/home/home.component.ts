import { Component, OnInit } from "@angular/core";

import { UserService } from "src/app/services/user.service";
import { ICustomer } from "src/app/models/customer";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  customer: ICustomer;
  isLoading: boolean = false;

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.isLoading = true;
    this.userService.getUser().subscribe((data) => {
      this.customer = data;
      this.isLoading = false;
    });
  }
}
