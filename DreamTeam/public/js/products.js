// Get Category
const getProducts = async ()=>{
    try {
        const results = await fetch('/data/products.json');
        const data = await results.json();
        const products = data.products;
        return products;
    } catch (err) {
        console.log(err)
    }
};
// Load Product
window.addEventListener('DOMContentLoaded', async()=>{
    const products = await getProducts();
    displayProductItems(products);
})

const categoryCenter = document.querySelector(".catgory__center");
// Display Product
const displayProductItems = items =>{
    let displayProduct = items.map(product=>
        `<div class="product category__product">
        <div class="product__header">
            <img src=${products.image} alt="">
        </div>
        <div class="product__footer">
            <h3>${products.title}</h3>
            <div class="rating">
                <i class="fas fa-star"></i>
                <i class="fas fa-star"></i>
                <i class="fas fa-star"></i>
                <i class="fas fa-star"></i>
                <i class="fas fa-star"></i>
            </div>
            <div class="product__price">
                <h4>$${products.price}</h4>
                <a href="#">
                    <button type="button" class="product__btn">
                    Add To Cart
                </button></a>
            </div>
            <ul>
                    <a href="">
                        <i class="fas fa-eye"></i>
                    </a>
                    <a href="">
                        <i class="fas fa-heart"></i>
                    </a> 
                    <a href="">
                        <i class="fas fa-undo"></i>
                    </a>
            </ul>
        </div>
    </div>`
    );
displayProduct = displayProduct.join(" ")
        if(categoryCenter){
            categoryCenter.innerHTML = displayProduct;
        }
};
// Filtering
const filterBtn = document.querySelectorAll('.filter-btn')
const categoryContainer = document.getElementById('.category')
if(categoryContainer){
    categoryContainer.addEventListener("click",async e=>{
        const target = e.target.closest(".section__title");
        if(!target) return;
        const id = target.dataset.id;
        const products = await getProducts();
        if(id){
            //remove active from button
            Array.from(filterBtn).forEach(btn=>{
                btn.classList.remove("active");

            });
            target.classList.add("active");
            //Load Products
            let menuCategory = products.filter(product=>{
                if(product.category == id){
                    return product;
                }
            });
            if(id == 'All Products'){
                displayProductItems(products);
            }else{
                displayProductItems(menuCategory);
            }
        }
    });
}