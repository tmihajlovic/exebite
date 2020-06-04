import { Injectable } from "@angular/core";
import { AuthService } from "angularx-social-login";
import { IUser } from "../models/user";
import { ICustomer } from "../models/customer";
import { map, switchMap } from "rxjs/operators";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class UserService {
  user: IUser;
  customers: ICustomer[];

  private customerDataUrl = (googleUserId) =>
    `http://localhost:6879/api/Customer/Query?GoogleUserId=${googleUserId}&Page=1&Size=100`;

  constructor(private authService: AuthService, private http: HttpClient) {}

  getUser() {
    return this.authService.authState.pipe(
      map((data) => ({
        email: data.email,
        photoUrl: data.photoUrl,
      })),
      switchMap((data: any) =>
        this.fetchCustomerData(data.email, data.photoUrl)
      )
    );
  }

  signOut(): void {
    this.authService.signOut();
  }

  fetchCustomerData(googleId, photoUrl) {
    return this.http
      .get<{ items: ICustomer[] }>(this.customerDataUrl(googleId))
      .pipe(map((items) => ({ ...items.items[0], photoUrl: photoUrl })));
  }
}
