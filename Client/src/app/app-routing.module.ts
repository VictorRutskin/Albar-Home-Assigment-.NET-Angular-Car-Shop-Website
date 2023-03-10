import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarsCardsComponent } from './Components/cars-cards/cars-cards.component';
import { AdminAddNewCarPageComponent } from './Pages/admin-pages/admin-add-new-car-page/admin-add-new-car-page.component';
import { AdminManageExistingCarsPageComponent } from './Pages/admin-pages/admin-manage-existing-cars-page/admin-manage-existing-cars-page.component';
import { AdminPageComponent } from './Pages/admin-pages/admin-page/admin-page.component';
import { AdminSingleCarEditPageComponent } from './Pages/admin-pages/admin-single-car-edit-page/admin-single-car-edit-page.component';
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
    component: CarsCardsComponent ,
  },
  {
    path: 'Cars/:filter',
    component: CarsCardsComponent,
  },
  {
    path: 'Cars/:id/Success',
    component: SuccessfullPurchasePageComponent,
  },
  {
    path: 'Cars/id/:id',
    component: SingleCarPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
