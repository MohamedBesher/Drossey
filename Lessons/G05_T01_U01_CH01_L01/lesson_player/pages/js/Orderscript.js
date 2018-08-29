    $(function(){
   
var adjustment;

$("ol.simple_with_animation").sortable({
  group: 'simple_with_animation',
  pullPlaceholder: false,
  // animation on drop
  onDrop: function  ($item, container, _super) {
      
     if($($item.get(0)).text() != "")
     {
    var $clonedItem = $('<li/>').css({height: 0});
    $item.before($clonedItem);
    $clonedItem.animate({'height': $item.height()});

    $item.animate($clonedItem.position(), function  () {
      $clonedItem.detach();
      _super($item, container);
    });
  }
      else
          {
            $($item.get(0)).remove()  
          }
  },

  // set $item relative to cursor position
  onDragStart: function ($item, container, _super) {
    var offset = $item.offset(),
        pointer = container.rootGroup.pointer;

    adjustment = {
      left: pointer.left - offset.left,
      top: pointer.top - offset.top
    };

    _super($item, container);
  },
  onDrag: function ($item, position) {
    $item.css({
      left: position.left - adjustment.left,
      top: position.top - adjustment.top
    });
  }
});
       
         //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            var MyObject = JSON.parse(data);
            
   
           $(MyObject).each(function(i,json){  
                        
                            var list= $(json.steps);
                          $('#temp_title').text(json.title);
                       
                 if(json.maintitle != undefined  && json.summary != undefined)
                     $('#title').append('<h4>' + json.maintitle + '</h4><hr/><h5>'+json.summary+'</h5>');
                       
                    
                          // random 
                        
                          var exists = [],randomNumber;
                            for(var l=0;l < list.length;l++) {
                            do {
                            randomNumber = Math.floor(Math.random()*list.length);  
                            } while (exists[randomNumber]);
                            exists[randomNumber] = true;
                                
                                list.eq(randomNumber).each(function(i,item){ 
                                    $(".simple_with_animation").append("<li>  <a href='#' class='list-group-item' id='"+randomNumber+"'>"+item.step+"</a></li>");
                                
                                })
                            
                            }
           })          

         }
        
        $("#correctbtn").click(function(){
    
    var flag=0;
     var result="إجابة صحيحة .. أحسنت";
    var arr_id=[];
    
    //get all id for chech order..
    $(".simple_with_animation a").each(function(index,item){
        arr_id.push($(this).prop("id"));        
    })
    
    //check order
    for(var i=0;i<arr_id.length;i++)
        {
            for(var j=i+1;j<=arr_id.length;j++)
        {
            
                        if(arr_id[i] > arr_id[j])
                            {
                               
                                result="إجابة خاطئة .. حاول مرة أخرى";
                                flag=1;
                                break;
                            }
                            
        }
            if(flag) break;
        }
    
    if(flag == 1)
    {
        $("#result_here").empty().append("<p class='alert alert-danger'>"+result+"</p>");
    }
    else
    {
        $("#result_here").empty().append("<p class='alert alert-success'>"+result+"</p>");
 
          
      
    }
              $('#myModal').modal('show'); 
})
      })             
  
    

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
