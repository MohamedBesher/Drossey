  $(function() {
      
var pointsArray = [];
var canvas = document.getElementById("myCanvas");
var context = canvas.getContext("2d");
var clickNo = 0;
function Point(x, y) {
    this.x = x;
    this.y = y;
    this.angle = 0;
}

canvas.onclick = drawDot;
function drawDot(e) {
    
    if(clickNo < 2)
        {
    var position = getMousePosition(canvas, e);
    posx = position.x;
    posy = position.y;
    storeCoordinate(posx, posy);
    context.fillStyle = "#000";
    context.fillRect(posx-3, posy-3, 6, 6);
          clickNo++;  
            if(clickNo == 2){
                clickNo=0;
        $("#solve").trigger("click");
            pointsArray=[];
            }
        }
}

function getMousePosition(c, e) {
    var rect = canvas.getBoundingClientRect();
    return {x: e.clientX - rect.left, y: e.clientY - rect.top}
}
function storeCoordinate(xVal, yVal) { pointsArray.push(new Point(xVal, yVal))}

$("#solve").click(
    function() {

      // find center
      var cent = findCenter(pointsArray);
      context.fillStyle = "green";
      context.fillRect(cent.x-3, cent.y-3, 6, 6);
      
      // find angles
     findAngles(cent, pointsArray);
      
      // sort based on angle using custom sort
      pointsArray.sort(function(a, b) {
        return (a.angle >= b.angle) ? 1 : -1
      });
            
      // draw lines
      context.beginPath();
      context.moveTo(pointsArray[0].x, pointsArray[0].y);
      //for(var i = 0; i < pointsArray.length; i++) {
        context.lineTo(pointsArray[1].x, pointsArray[1].y);
      //}
      context.strokeStyle = "#00f";
      context.closePath();
      context.stroke();
    }
);

function findCenter(points) {
  var x = 0, y = 0, i, len = points.length;
  for (i = 0; i < len; i++) {
    x += points[i].x;
    y += points[i].y;
  }
  return {x: x / len, y: y / len};   // return average position
}

function findAngles(c, points) {
  var i, len = points.length, p, dx, dy;
  for (i = 0; i < len; i++) {
    p = points[i];
    dx = p.x - c.x;
    dy = p.y - c.y;
    p.angle = Math.atan2(dy, dx);
  }
}

$("#reset").click(
  function() {
      context.clearRect(0, 0, canvas.width, canvas.height); //clear the canvas
      pointsArray = []; //clear the array
  }
);
});
