import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { HttpClientModule } from "@angular/common/http";
import { SocialLoginModule, AuthServiceConfig } from "angularx-social-login";
import { GoogleLoginProvider } from "angularx-social-login";
import { AppComponent } from "./app.component";
import { LoginComponent } from "./components/login/login.component";
import { HomeComponent } from "./components/home/home.component";
import { UserService } from "./services/user.service";
import { RestaurantService } from "./services/restaurant.service";
import { RestaurantListComponent } from './components/restaurant-list/restaurant-list.component';
import { RestaurantDetailComponent } from './components/restaurant-detail/restaurant-detail.component';

let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider(
      "110458096179-dfcud7kgfna7m8it9qdnis27kmdh5rrj.apps.googleusercontent.com"
    ),
  },
]);

export function provideConfig() {
  return config;
}

@NgModule({
  declarations: [AppComponent, LoginComponent, HomeComponent, RestaurantListComponent, RestaurantDetailComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SocialLoginModule,
    HttpClientModule,
  ],
  providers: [
    UserService,
    RestaurantService,
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
