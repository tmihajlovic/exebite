import { Component, OnInit, OnDestroy } from "@angular/core";
import { IRestaurant } from "src/app/models/restaurant";
import { ActivatedRoute, Params } from "@angular/router";
import { Subscription } from "rxjs";
import { FoodService } from "src/app/services/food.service";
import { IFood } from "src/app/models/food";

@Component({
  selector: "app-restaurant-detail",
  templateUrl: "./restaurant-detail.component.html",
  styleUrls: ["./restaurant-detail.component.scss"],
})
export class RestaurantDetailComponent implements OnInit, OnDestroy {
  restaurant: IRestaurant;
  foodsData: IFood[] = [];
  id: number;
  totalData = {
    id: 0,
    name: "",
    price: 0,
    restaurantId: 0,
  };
  private sub: Subscription;

  constructor(
    private foodService: FoodService,
    private route: ActivatedRoute
  ) {}
  ngOnInit() {
    this.sub = this.route.params.subscribe((params: Params) => {
      this.id = +params["id"];
      this.foodService.fetchFoodForRestaurant(this.id).subscribe((foods) => {
        this.foodsData = foods;
      });
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
