import { Injectable } from "@angular/core";
import { AuthService } from "angularx-social-login";
import { IUser } from "./user";
import { ICustomer } from "./customer";
import { map } from "rxjs/operators";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class UserService {
  user: IUser;
  customers: ICustomer[];

  private customerDataUrl =
    "http://localhost:6879/api/Customer/Query?Page=1&Size=100";

  constructor(private authService: AuthService, private http: HttpClient) {}

  getUser() {
    return this.authService.authState.pipe(
      map((data) => {
        let userData;
        if (data) {
          userData = {
            name: data.name,
            email: data.email,
            photoUrl: data.photoUrl,
          };
        }
        return userData;
      })
    );
  }

  signOut(): void {
    this.authService.signOut();
  }

  fetchCustomerData() {
    return this.http.get<{ items: ICustomer[] }>(this.customerDataUrl).pipe(
      map(({ items }) => ({
        customers: items,
      }))
    );
  }
}
