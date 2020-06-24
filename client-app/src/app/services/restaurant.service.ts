import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IRestaurant } from "../models/restaurant";
import { map } from "rxjs/operators";

@Injectable()
export class RestaurantService {
  constructor(private http: HttpClient) {}

  private restaurantsDataUrl =
    "http://localhost:6879/api/restaurant/Query?Page=1&Size=5";

  private restaurantDataUrl = (id) =>
    `http://localhost:6879/api/restaurant/Query?id=${id}&Page=1&Size=5`;

  fetchRestaurants() {
    return this.http
      .get<{ items: IRestaurant[] }>(this.restaurantsDataUrl)
      .pipe(
        map((data) => ({
          restaurants: data.items,
        }))
      );
  }

  fetchDataForRestaurant(id) {
    return this.http
      .get<{ items: IRestaurant }>(this.restaurantDataUrl(id))
      .pipe(map((items) => ({ ...items.items[0] })));
  }
}
