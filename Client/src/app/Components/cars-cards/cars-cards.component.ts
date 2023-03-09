import { Component, OnInit } from '@angular/core';
import { CarsService } from 'src/app/Services/Cars/cars.service';
import { Car } from './../../Models/Car.model';

@Component({
  selector: 'cars-cards',
  templateUrl: './cars-cards.component.html',
  styleUrls: ['./cars-cards.component.scss'],
})
export class CarsCardsComponent implements OnInit {
  // Create new empty cars list
  cars: Car[] = [];
  constructor(private carsService: CarsService) {}

  ngOnInit(): void {
    this.carsService.GetAllCars().subscribe({
      next: (cars) => {
        this.cars = cars;
        console.log(cars);
      },
      error: (response) => {
        console.log(response);
      },
    });
  }
}
