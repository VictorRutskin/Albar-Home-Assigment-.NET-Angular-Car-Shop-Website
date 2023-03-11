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
        // Get the car images
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
        
        // Set the cars array and initialize the component
        this.cars = cars;
        this.isInitialized = true;
      },
      error: (response) => {
        console.log(response);
      },
    });
  }
}