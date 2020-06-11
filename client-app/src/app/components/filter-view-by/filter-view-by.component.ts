import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-filter-view-by",
  templateUrl: "./filter-view-by.component.html",
  styleUrls: ["./filter-view-by.component.scss"],
})
export class FilterViewByComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {}

  showRestorauntList() {
    this.router.navigate(["/home"]);
  }
}
