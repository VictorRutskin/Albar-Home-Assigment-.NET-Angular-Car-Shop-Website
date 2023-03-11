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
    private router: Router
  ) {}

  carId: number = 0;

  car: Car = {
    id: 0,
    name: "",
    category: "",
    price: 0,
    unitsInStock: 0,
    modelYear: 0,
    imageSrc: "",
  };

  updateCar() {
    this.carsService.PutUpdateCar(this.car).subscribe({
      next: () => {
        location.reload();
      },
      error: (error: any) => {
        console.error("Error occurred while updating car:", error);
        // Display an error message to the user
        alert(
          "An error occurred while updating the car.\n\nPlease make sure you have entered all the fields correctly!"
        );
      },
    });
  }

  ngOnInit() {
    this.route.paramMap.subscribe((params: any) => {
      const idString = params.get("id");
      if (idString) {
        this.carId = parseInt(idString, 10); // convert string to number
        console.log(this.carId); // log the carId to the console
      }
    });

    this.carsService.GetSingleCar(this.carId).subscribe({
      next: (car: any) => {
        this.car = car;
        this.carsService.GetImage(car.id).subscribe({
          next: (imageData: Blob) => {
            car.imageSrc = URL.createObjectURL(imageData);
          },
          error: (error: any) => {
            console.error(
              "Error occurred while getting car image:",
              error
            );
            // Display an error message to the user
            alert(
              "An error occurred while getting the car image.\n\nPlease try again later."
            );
          },
        });
      },
      error: (error: any) => {
        console.error("Error occurred while getting car details:", error);
        // Display an error message to the user
        alert(
          "An error occurred while getting the car details.\n\nPlease try again later."
        );
      },
    });
  }
}
