import { Injectable } from "@angular/core";
import { IUser } from "../models/user";
import { ICustomer } from "../models/customer";
import { map } from "rxjs/operators";
import { HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment';

@Injectable()
export class UserService {
  user: IUser;
  customers: ICustomer[];

  private customerDataUrl = (googleUserId) =>
    `${environment.backendBaseAPIUrl}/Customer/Query?GoogleUserId=${googleUserId}&Page=1&Size=100`;

  constructor(private http: HttpClient) { }

  fetchCustomerData(googleId: string, photoUrl: string) {
    return this.http
      .get<{ items: ICustomer[] }>(this.customerDataUrl(googleId))
      .pipe(map((items) => ({ ...items.items[0], photoUrl: photoUrl })));
  }

}
