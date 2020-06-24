import { Component, OnInit } from "@angular/core";
import { UserService } from "src/app/services/user.service";
import { Router } from "@angular/router";
import { ICustomer } from "src/app/models/customer";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"],
})
export class HeaderComponent implements OnInit {
  isLoading: boolean = false;
  customer: ICustomer;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.userService.getUser().subscribe((data) => {
      this.customer = data;
      this.isLoading = false;
    });
  }

  signOutNavigate(): void {
    this.userService.signOut();
    this.router.navigate(["/"]);
  }
}
