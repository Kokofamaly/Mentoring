const cars = ["Saab", "Volvo", "BMW"];

const bmw = cars.find(c => c === "BMW");

if(bmw){
    console.log(bmw);
}


cars[0] = "Suzuki";
console.log(cars[0]);


const lastItem = cars.pop();
if(lastItem) {
    console.log(lastItem);
}


cars.push("Audi");
console.log(cars);


cars.splice(1, 1, "BMW");
console.log(cars);