import { Component, OnInit, OnDestroy } from "@angular/core";

import { UserService } from "src/app/services/user.service";
import { ICustomer } from "src/app/models/customer";
import { Subscription } from "rxjs";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit, OnDestroy {
  customer: ICustomer;
  isLoading: boolean = false;
  private sub: Subscription;

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.isLoading = true;
    this.sub = this.userService.getUser().subscribe((data) => {
      this.customer = data;
      this.isLoading = false;
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
