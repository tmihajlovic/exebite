import { Component, OnInit, OnDestroy } from "@angular/core";
import { IRestaurant } from "src/app/models/restaurant";
import { RestaurantService } from "src/app/services/restaurant.service";
import { ActivatedRoute, Params } from "@angular/router";
import { Subscription } from "rxjs";

@Component({
  selector: "app-restaurant-detail",
  templateUrl: "./restaurant-detail.component.html",
  styleUrls: ["./restaurant-detail.component.scss"],
})
export class RestaurantDetailComponent implements OnInit, OnDestroy {
  restaurant: IRestaurant;
  id: number;
  private sub: Subscription;

  constructor(
    private restaurantService: RestaurantService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.sub = this.route.params.subscribe((params: Params) => {
      this.id = +params["id"];
      this.restaurantService
        .fetchDataForRestaurant(this.id)
        .subscribe((data) => {
          this.restaurant = data;
        });
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
