import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-filter-view-by",
  templateUrl: "./filter-view-by.component.html",
  styleUrls: ["./filter-view-by.component.scss"],
})
export class FilterViewByComponent implements OnInit {
  isValue: number = 1;
  constructor(private router: Router) {}

  ngOnInit(): void {}

  showRestorauntList() {
    this.isValue = 1;
    this.router.navigate(["/home/1"]);
  }
}
