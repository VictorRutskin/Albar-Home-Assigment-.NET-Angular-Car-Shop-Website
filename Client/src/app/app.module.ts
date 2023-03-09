import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { MainPageComponent } from './Pages/main-page/main-page.component';
import { FooterComponent } from './Components/footer/footer.component';
import { FamilyCarsPageComponent } from './Pages/cars-pages/family-cars-page/family-cars-page.component';
import { MiniCarsPageComponent } from './Pages/cars-pages/mini-cars-page/mini-cars-page.component';
import { TruckCarsPageComponent } from './Pages/cars-pages/truck-cars-page/truck-cars-page.component';
import { LuxuryCarsPageComponent } from './Pages/cars-pages/luxury-cars-page/luxury-cars-page.component';
import { SportsCarsPageComponent } from './Pages/cars-pages/sports-cars-page/sports-cars-page.component';
import { SuvCarsPageComponent } from './Pages/cars-pages/suv-cars-page/suv-cars-page.component';
import { AllCarsPageComponent } from './Pages/cars-pages/all-cars-page/all-cars-page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    MainPageComponent,
    FooterComponent,
    FamilyCarsPageComponent,
    MiniCarsPageComponent,
    TruckCarsPageComponent,
    LuxuryCarsPageComponent,
    SportsCarsPageComponent,
    SuvCarsPageComponent,
    AllCarsPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
