import { Component, OnInit, Input, OnDestroy } from "@angular/core";
import { IRestaurant } from "src/app/models/restaurant";
import { RestaurantService } from "src/app/services/restaurant.service";
import { Subscription } from "rxjs";

@Component({
  selector: "app-restaurant-list",
  templateUrl: "./restaurant-list.component.html",
  styleUrls: ["./restaurant-list.component.scss"],
})
export class RestaurantListComponent implements OnInit, OnDestroy {
  // @Input() id: number;
  dataRestaurants: IRestaurant[];
  private sub: Subscription;

  constructor(private restaurantService: RestaurantService) {}

  ngOnInit() {
    this.sub = this.restaurantService
      .fetchRestaurants()
      .subscribe(({ restaurants }) => {
        this.dataRestaurants = restaurants;
      });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
