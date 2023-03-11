import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from 'src/app/Models/Car.model';
import { CarsService } from 'src/app/Services/Cars/cars.service';

@Component({
  selector: 'app-admin-add-new-car-page',
  templateUrl: './admin-add-new-car-page.component.html',
  styleUrls: ['./admin-add-new-car-page.component.scss'],
})
export class AdminAddNewCarPageComponent {
  // This page at first adds car details without image,
  // then lets user to upload image after it gets the given id to the car by the backend,
  // adding image is an option not a must.

  constructor(
    private route: ActivatedRoute,
    private carsService: CarsService,
    private router: Router
  ) {}
  carId: number = 0;

  ShowImageButton: boolean = false;

  //// Regex for potential future features (input css color change on value change etc)
  // RegexPatterns = [
  //   { name: 'name', pattern: /^[a-zA-Z ]{1,50}$/ }, // Maximum of 50 characters and no numbers
  //   { name: 'price', pattern: /^(?!0)\d{1,7}$/  }, // Number between 1-9999999:
  //   { name: 'unitsInStock', pattern: /^(?:0|[1-9]\d{0,2}|1000)$/ }, // Number between 0-1000:
  //   { name: 'modelYear', pattern: /^(188[6-9]|189\d|19\d{2}|20[01]\d|202[0-3])$/ }, // Number from 1886-2023
  // ];

  formData: any = {
    name: '',
    price: 0,
    unitsInStock: 0,
    modelYear: 0,
  };

  car: Car = {
    id: 0,
    name: '',
    category: '',
    price: 0,
    unitsInStock: 0,
    modelYear: 0,
    imageSrc: '',
  };
  async newCar(form: NgForm) {
    // updating car values using form
    this.car.name = form.value.name;
    this.car.category = form.value.category;
    this.car.price = form.value.price;
    this.car.unitsInStock = form.value.unitsInStock;
    this.car.modelYear = form.value.modelYear;

    await this.carsService.PostNewCar(this.car).subscribe({
      next: (response) => {
        console.log(response);
        // getting the new car given id and then letting user upload the image
        this.carsService.GetSingleCar2(this.car).subscribe({
          next: (car) => {
            this.car = car;
            this.carId = this.car.id;
            console.log(this.carId);
            this.ShowImageButton = true;
          },
          error: (error) => {
            console.error(
              'Error occurred while getting the new car ID:',
              error
            );
            // Display an error message to the user
            alert(
              'An error occurred while getting the new car ID. Please try again later.'
            );
          },
        });
      },
      error: (error) => {
        console.error('Error occurred while adding new car:', error);
        // Display an error message to the user
        alert(
          'An error occurred while adding the new car.\n\nPlease Make sure you have entered all the fields correctly!\n\nAnd make sure this car doesnt already exist in the shop! '
        );
      },
    });
  }
}
