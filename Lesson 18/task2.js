// define array with the following values "Saab", "Volvo", "BMW"
const cars = ["Saab", "Volvo", "BMW"];


// get "BMW" value 
const bmw = cars.find(c => c === "BMW");
if(bmw){ console.log(bmw); }


// change the first item of cars 
cars[0] = "Suzuki";
console.log(cars[0]);


// remove last item in the array 
const lastItem = cars.pop();
if(lastItem) { console.log(lastItem); }


// add "Audi" to the array 
cars.push("Audi");
console.log(cars);


// splice "Volvo" and "BMW" 
cars.splice(1, 1, "BMW");
console.log(cars);