import { Component, Input, OnInit } from '@angular/core';
import { CarsService } from 'src/app/Services/Cars/cars.service';
import { Car } from './../../Models/Car.model';

@Component({
  selector: 'cars-cards',
  templateUrl: './cars-cards.component.html',
  styleUrls: ['./cars-cards.component.scss']
})
export class CarsCardsComponent implements OnInit {
  cars: Car[] = [];
  filteredCars: Car[] = [];

  //holds filter string
  @Input() filterValue: string = '';
  constructor(private carsService: CarsService) {}

  ngOnInit(): void {
    this.carsService.GetAllCars().subscribe({
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
        if (this.filterValue != 'All') {
          this.filteredCars = this.cars.filter(
            (car) => car.category === this.filterValue
          );
        } else {
          this.filteredCars = this.cars;
        }
        console.log(cars);
      },
      error: (response) => {
        console.log(response);
      },
    });
  }
}
