import { Component } from '@angular/core';
import { Car } from 'src/app/Models/Car.model';

@Component({
  selector: 'app-admin-single-car-edit-page',
  templateUrl: './admin-single-car-edit-page.component.html',
  styleUrls: ['./admin-single-car-edit-page.component.scss']
})
export class AdminSingleCarEditPageComponent {
  car: Car = {
    id: 0,
    name: '',
    category: '',
    price: 0,
    unitsInStock: 0,
    modelYear: 0,
    imageSrc: '',
  };
  
}

