$(function(){
    
    
        // get data from Json and print theme
        
     //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
    
          var MyObject = JSON.parse(data);
        
           
            $(MyObject).each(function(i,json){  
            
           if(json.title != undefined  && json.summary != undefined)
                     $('#title').append('<h4>' + json.title + '</h4><hr/><h5>'+json.summary+'</h5>');
                        
                 $(json.equations).each(function(j,text){  
             
                        //Icon slot
                     $("#text_div").append('<div class="item col-md-12"><span class="firstside col-md-5">'+text[0]+'</span><span class="OP col-md-1"><div id="'+text[1]+'" class="ui-widget-header" > </div></span><span class="secondside col-md-5">'+text[2]+'</span></div>');
                     //print text here 
                     
            
        });

                //Icon Card
                    $(json.operation).each(function(h,element){
                         $("#Icon_container").append('<div class="icon ui-widget-content"  id="'+element+'">'+element+'</div>');
                        
                        
                    })
                    
        });
   

         
    
    // drag drop code 
      var  lastclicked;
    
    // make card draggable
    $( ".icon" ).draggable({ revert: "invalid" , drag: function(event, ui) { lastclicked = event.target.id} });
        
    
    // make slot droppable   
         $(".OP div").droppable({
      classes: {
        "ui-droppable-active": "ui-state-active",
        "ui-droppable-hover": "ui-state-hover"
      },
      drop:handleCardDrop,
            
      accept: function(draggableElement) {
          if($(draggableElement).attr("id")==$(this).attr("id"))
          {
                 return true;
    }},
    
     // hoverClass: 'hovered',
    
    });
       
   
    
    
    }
})
    
function handleCardDrop( event, ui ) {
  var slot = $(this).attr("id");
  var card = ui.draggable.attr("id");

  // If the card was dropped to the correct slot,
  // change the card colour, position it directly
  // on top of the slot, and prevent it being dragged
  // again

  if ( slot == card ) {
    ui.draggable.draggable( 'disable' );
    $(this).droppable( 'disable' );
    ui.draggable.position( { of: $(this), my: 'left top', at: 'left top' } );
    ui.draggable.draggable( 'option', 'revert', false );
    $(ui.draggable).css("background-color","maroon");
      $(ui.draggable).css("z-index","10");
    $(this).css("background-color","maroon");
  } 
  
  // If all the cards have been placed correctly then display a message
  // and reset the cards for another go ..

 

}

function loadScript(path, callback) {
 
    var done = false;
    var scr = document.createElement('script');
    scr.onload = handleLoad;
    scr.onreadystatechange = handleReadyStateChange;
    scr.onerror = handleError;
    scr.src = path;
    document.body.appendChild(scr);

    function handleLoad() {
        if (!done) {
            done = true;
            callback(path, "ok");
        }
    }

    function handleReadyStateChange() {
        var state;
        if (!done) {
            state = scr.readyState;
            if (state === "complete") {
                handleLoad();
            }
        }
    }

    function handleError() {
        if (!done) {
            done = true;
            callback(path, "error");
        }
    }
    return scr;
}
