import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-filter-view-by",
  templateUrl: "./filter-view-by.component.html",
  styleUrls: ["./filter-view-by.component.scss"],
})
export class FilterViewByComponent implements OnInit {
  isFilterBy: string = "restaurant";
  constructor(private router: Router) {}

  ngOnInit(): void {}

  showRestorauntList() {
    this.isFilterBy = "restaurant";
    this.router.navigate(["/home/1"]);
  }
}
