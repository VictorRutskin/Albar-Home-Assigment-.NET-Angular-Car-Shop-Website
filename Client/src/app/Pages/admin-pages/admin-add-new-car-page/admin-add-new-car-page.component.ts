import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from 'src/app/Models/Car.model';
import { CarsService } from 'src/app/Services/Cars/cars.service';

@Component({
  selector: 'app-admin-add-new-car-page',
  templateUrl: './admin-add-new-car-page.component.html',
  styleUrls: ['./admin-add-new-car-page.component.scss'],
})
export class AdminAddNewCarPageComponent {
  // This page at first adds a car without image,
  // then lets user to upload image after it gets the given id to the car by the backend
  
  constructor(
    private route: ActivatedRoute,
    private carsService: CarsService,
    private router:Router,
  ) {}
  carId: number = 0;

  ShowImageButton:boolean=false;

  car: Car = {
    id: 0,
    name: '',
    category: '',
    price: 0,
    unitsInStock: 0,
    modelYear: 0,
    imageSrc: '',
  };
  newCar() {
    this.carsService.PostNewCar(this.car)
    .subscribe({
      next: (response) =>{
        console.log(response); 
         }
    });
    // getting the new car given id and then letting user upload the image
    this.carsService.GetSingleCar2(this.car)
    .subscribe({
      next: (car) =>{
        this.car=car;
        this.carId==this.car.id;
        this.ShowImageButton=true;  
         }
    });
  }
}