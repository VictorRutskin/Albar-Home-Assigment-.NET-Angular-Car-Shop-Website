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
import { CarouselComponent } from './Components/carousel/carousel.component';
import { CarsCardsComponent } from './Components/cars-cards/cars-cards.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UploadImageComponent } from './Components/upload-image/upload-image.component';
import { SafeResourceUrlPipe } from './Pipes/SafeResourceUrl/safe-resource-url.pipe';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { SingleCarPageComponent } from './Pages/single-car-page/single-car-page.component';
import { SuccessfullPurchasePageComponent } from './Pages/successfull-purchase-page/successfull-purchase-page.component';
import { AdminPageComponent } from './Pages/admin-pages/admin-page/admin-page.component';
import { AdminSingleCarEditPageComponent } from './Pages/admin-pages/admin-single-car-edit-page/admin-single-car-edit-page.component';

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
    AllCarsPageComponent,
    CarouselComponent,
    CarsCardsComponent,
    UploadImageComponent,
    SafeResourceUrlPipe,
    SingleCarPageComponent,
    SuccessfullPurchasePageComponent,
    AdminPageComponent,
    AdminSingleCarEditPageComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    BrowserAnimationsModule,
    NgbCarouselModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
