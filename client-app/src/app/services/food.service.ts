import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IFood } from "../models/food";
import { map } from "rxjs/operators";

@Injectable()
export class FoodService {
  constructor(private http: HttpClient) {}

  private foodsDataUrl = "http://localhost:6879/api/food/Query?Page=1&Size=30";

  private restFoodDataUrl = (id) =>
    `http://localhost:6879/api/food/Query?RestaurantId=${id}&Page=1&Size=30`;

  fetchFoods() {
    return this.http.get<{ items: IFood[] }>(this.foodsDataUrl).pipe(
      map((data) => ({
        foods: data.items,
      }))
    );
  }

  fetchFoodForRestaurant(id) {
    return this.http
      .get<{ items: IFood[] }>(this.restFoodDataUrl(id))
      .pipe(map((items) => [...items.items]));
  }
}
