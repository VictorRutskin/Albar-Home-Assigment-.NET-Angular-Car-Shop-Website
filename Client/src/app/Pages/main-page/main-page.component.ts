import { Component, OnInit } from '@angular/core';
import { environment } from 'src/app/Environments/myEnvironment';
import { Car } from 'src/app/Models/Car.model';
import { CarsService } from 'src/app/Services/Cars/cars.service';

@Component({
  selector: 'main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit {
  response = {dbPath: ''};
  cars: Car[] | undefined;

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
