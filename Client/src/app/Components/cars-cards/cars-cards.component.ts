import { Component, OnInit } from '@angular/core';
import { CarsService } from 'src/app/Services/Cars/cars.service';
import { Car } from './../../Models/Car.model';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';



@Component({
  selector: 'cars-cards',
  templateUrl: './cars-cards.component.html',
  styleUrls: ['./cars-cards.component.scss'],
})
export class CarsCardsComponent implements OnInit {
  // Create new empty cars list
  cars: Car[] = [];
  constructor(private carsService: CarsService,private http: HttpClient,private sanitizer: DomSanitizer) {}

  ngOnInit(): void {
    this.carsService.GetAllCars().subscribe({
      next: (cars) => {
        this.cars = cars;
        cars.forEach(car => {
          this.carsService.GetImage(car.id).subscribe({
            next: (imageData: Blob) => {
              car.imageSrc = URL.createObjectURL(imageData);
            },
            error: (error: any) => {
              console.error(error);
            }
          });
        });
        console.log(cars);
      },
      error: (response) => {
        console.log(response);
      },
    });
  }
}  