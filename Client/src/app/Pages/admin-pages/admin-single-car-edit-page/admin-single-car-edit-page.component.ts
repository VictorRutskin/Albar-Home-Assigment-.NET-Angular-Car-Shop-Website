import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from 'src/app/Models/Car.model';
import { CarsService } from 'src/app/Services/Cars/cars.service';

@Component({
  selector: 'app-admin-single-car-edit-page',
  templateUrl: './admin-single-car-edit-page.component.html',
  styleUrls: ['./admin-single-car-edit-page.component.scss']
})
export class AdminSingleCarEditPageComponent {
  constructor(
    private route: ActivatedRoute,
    private carsService: CarsService,
    private router:Router,
  ) {}

  carId: number = 0;
  
  car: Car = {
    id: 0,
    name: '',
    category: '',
    price: 0,
    unitsInStock: 0,
    modelYear: 0,
    imageSrc: '',
  };
  
  updateCar(){
    this.carsService.PostBuyOne(this.car)
    .subscribe({
      next: (response) =>{
         location.reload();
      }
    });
  }

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      const idString = params.get('id');
      if (idString) {
        this.carId = parseInt(idString, 10); // convert string to number
        console.log(this.carId); // log the carId to the console
      }
    });

    this.carsService.GetSingleCar(this.carId).subscribe({
      next: (car) => {
        this.car = car;
        this.carsService.GetImage(car.id).subscribe({
          next: (imageData: Blob) => {
            car.imageSrc = URL.createObjectURL(imageData);
          },
          error: (error: any) => {
            console.error(error);
          },
        });
      },
    });
  }
}
