import { Injectable } from "@angular/core";
import { AuthService } from "angularx-social-login";
import { IUser } from "./user";
import { map } from "rxjs/operators";

@Injectable()
export class UserService {
  user: IUser;

  constructor(private authService: AuthService) {}

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
}
