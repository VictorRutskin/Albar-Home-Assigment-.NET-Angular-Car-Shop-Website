import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAddNewCarPageComponent } from './Pages/admin-pages/admin-add-new-car-page/admin-add-new-car-page.component';
import { AdminManageExistingCarsPageComponent } from './Pages/admin-pages/admin-manage-existing-cars-page/admin-manage-existing-cars-page.component';
import { AdminPageComponent } from './Pages/admin-pages/admin-page/admin-page.component';
import { AdminSingleCarEditPageComponent } from './Pages/admin-pages/admin-single-car-edit-page/admin-single-car-edit-page.component';
import { AllCarsPageComponent } from './Pages/cars-pages/all-cars-page/all-cars-page.component';
import { FamilyCarsPageComponent } from './Pages/cars-pages/family-cars-page/family-cars-page.component';
import { LuxuryCarsPageComponent } from './Pages/cars-pages/luxury-cars-page/luxury-cars-page.component';
import { MiniCarsPageComponent } from './Pages/cars-pages/mini-cars-page/mini-cars-page.component';
import { SportsCarsPageComponent } from './Pages/cars-pages/sports-cars-page/sports-cars-page.component';
import { SuvCarsPageComponent } from './Pages/cars-pages/suv-cars-page/suv-cars-page.component';
import { TruckCarsPageComponent } from './Pages/cars-pages/truck-cars-page/truck-cars-page.component';
import { MainPageComponent } from './Pages/main-page/main-page.component';
import { SingleCarPageComponent } from './Pages/single-car-page/single-car-page.component';
import { SuccessfullPurchasePageComponent } from './Pages/successfull-purchase-page/successfull-purchase-page.component';

const routes: Routes = [
  {
    path: '',
    component: MainPageComponent,
  },
  {
    path: 'Home',
    component: MainPageComponent,
  },
  {
    path: 'Admin',
    component: AdminPageComponent,
  },
  {
    path: 'Admin/Manage',
    component: AdminManageExistingCarsPageComponent,
  },
  {
    path: 'Admin/Add',
    component: AdminAddNewCarPageComponent,
  },
  {
    path: 'Admin/Edit/:id',
    component: AdminSingleCarEditPageComponent,
  },
  {
    path: 'Cars',
    component: AllCarsPageComponent,
  },
  {
    path: 'Cars/Family',
    component: FamilyCarsPageComponent,
  },
  {
    path: 'Cars/Mini',
    component: MiniCarsPageComponent,
  },
  {
    path: 'Cars/Truck',
    component: TruckCarsPageComponent,
  },
  {
    path: 'Cars/Luxury',
    component: LuxuryCarsPageComponent,
  },
  {
    path: 'Cars/Sports',
    component: SportsCarsPageComponent,
  },
  {
    path: 'Cars/SUV',
    component: SuvCarsPageComponent,
  },
  {
    path: 'Cars/:id/Success',
    component: SuccessfullPurchasePageComponent,
  },
  {
    path: 'Cars/:id',
    component: SingleCarPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
