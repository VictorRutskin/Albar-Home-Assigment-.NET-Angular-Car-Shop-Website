import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/Models/Car.model';
import { CarsService } from 'src/app/Services/Cars/cars.service';
@Component({
  selector: 'carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss'],
})
export class CarouselComponent implements OnInit {
  cars: Car[] = [];
  isInitialized = false;
  constructor(private carsService: CarsService) {}

  ngOnInit(): void {
    this.carsService.Get3CarExtras().subscribe({
      next: (cars) => {
        this.cars = cars;
        cars.forEach((car) => {
          this.carsService.GetImage(car.id).subscribe({
            next: (imageData: Blob) => {
              car.imageSrc = URL.createObjectURL(imageData);
            },
            error: (error: any) => {
              console.error(error);
            },
          });
        });
        // This checks if not all, add filter, else the filtered array will be all the cars
        console.log(cars);
        this.isInitialized = true; // move the assignment inside the subscribe method
      },
      error: (response) => {
        console.log(response);
      },
    });
  }
}
