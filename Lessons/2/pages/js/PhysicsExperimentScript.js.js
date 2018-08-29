   $(document).ready(function(){
       
       //to show tooltips ( word which appear when hover on element )
  $('[data-toggle="tooltip"]').tooltip();   
});



   jsPlumb.ready(function() {
     
            var common = {
              isSource:true,
              isTarget:true,
              endpoint: [ "Dot", { radius:7 } ]
                //,
                //connector : "Straight"
            };
           
     $("#diagramContainer div").each(function(index,element)
     {
         if( element.id == "lampdiv"  )
             {
                 jsPlumb.addEndpoint(element.id, { 
             anchor:"BottomLeft" ,
              
              hoverPaintStyle:{ fillStyle:"grey" }
            }, common);

            jsPlumb.addEndpoint(element.id, { 
              anchor:"BottomRight" ,
            hoverPaintStyle:{ fillStyle:"grey" }
            }, common);
             }
         else
             {
         jsPlumb.addEndpoint(element.id, { 
             anchor:"LeftMiddle" ,
              cssClass:"endpointClass",
             hoverPaintStyle:{ fillStyle:"grey" }
            }, common);

            jsPlumb.addEndpoint(element.id, { 
              anchor:"RightMiddle" ,
                 cssClass:"endpointClass",
                hoverPaintStyle:{ fillStyle:"grey" }
            }, common);
             }

     
        jsPlumb.draggable(element.id);
       //  $('#'+element.id).draggable({ containment: $('.container')});
         $('#'+element.id).draggable({
            
                start: function(event, ui) {
                    
                    //to hide endpoints ..
                    HidePoints(element);
               
                }, // console.log(event);console.log(ui)},
                stop: function(event, ui) {}, // console.log(event);//console.log(ui)},
                cursor:'move',
                opacity: 0.5,
             containment: $('.container')
            });
         
         
         //to hide endpoints .. 
         HidePoints(element);
         
    });
         
       $( "#drop_here" ).droppable({
            drop: function( event, ui ) {
               
              
                //to show endpoint 
                 var eps=jsPlumb.getEndpoints(ui.draggable.get(0));
                for(var j=0;j<eps.length;j++)
                {
                    eps[j].setVisible(true);   // Set visibility of endpoint to false
                }
                
                  //to return endpoint to correct position..
                    jsPlumb.repaintEverything();
                
            }
         });  
     
     
       $( "#mytools" ).droppable({
            drop: function( event, ui ) {
               
              
            // Detach connectors ..
                
                //Get connector which has source id = draggable element .
                var conn1 = jsPlumb.getConnections({source:ui.draggable.get(0).id});
                
                if(conn1.length > 0)
                {
                  
                
                //Detach these connectors
                for(var k=0;k<conn1.length;k++)
                    jsPlumb.detach(conn1[k]);
                    
                //loop to get endpoints of all connector to show endpoints of non-dragaable element. 

                $(conn1).each(function(index,item){
                
                 var eps=jsPlumb.getEndpoints($("#"+item.targetId));
                for(var j=0;j<eps.length;j++)
                {
                    eps[j].setVisible(true);   
                }
                 
                 })
                }
                                   
                //Get connector which has target id = draggable element .
                  var conn2 =jsPlumb.getConnections({target:ui.draggable.get(0).id});
                
                if(conn2.length > 0)
                {
                 
                
                //Detach these connectors
                 for(var m=0;m<conn2.length;m++)
                    jsPlumb.detach(conn2[m]);
                 
                //loop to get endpoints of all connector to show endpoints of non-dragaable element. 
               $(conn2).each(function(index,item){
                
                var eps2=jsPlumb.getEndpoints($("#"+item.sourceId));
                   
                for(var j=0;j<eps2.length;j++)
                {
                    eps2[j].setVisible(true);   
                }
                   
                })
                  
               
                }
                 
                
                
                
                  //to return endpoint to correct position..
                    jsPlumb.repaintEverything();
         
                /**********************/
                
                //to hide endpoint of draggable element 
                 var eps=jsPlumb.getEndpoints(ui.draggable.get(0));
                for(var j=0;j<eps.length;j++)
                {
                    eps[j].setVisible(false);   // Set visibility of endpoint to false
                }
                
                
            }
         });  
       
     })

   
 $( function() {
             
      // الإرشادات pop up
     
        $(".modal-footer").append('<button type="button" class="btn btn-success" data-dismiss="modal">ابدأ التجربة</button>');
       $("#myModal").modal('show');
     
        
     
         jsPlumb.bind("connection", function(info) {
             
        /*
        //get connection from event info
            var connection = info.connection;

            //add on click event
            connection.bind("click", function(conn) {
                jsPlumb.detach(conn);
            });
            
            */
             
             if(!$($("#"+ info.sourceId).get(0)).hasClass("on2"))
                 {
                     if($($("#"+ info.sourceId).get(0)).hasClass("on1"))
                     {
                        $($("#"+ info.sourceId).get(0)).addClass("on2");
                     }
                     else
                    {
                        $($("#"+ info.sourceId).get(0)).addClass("on1");
                    }
                 }
                 
                 if(!$($("#"+ info.targetId).get(0)).hasClass("on2"))
                 {
                 if($($("#"+ info.targetId).get(0)).hasClass("on1"))
                 {
                     $($("#"+ info.targetId).get(0)).addClass("on2");
                 }
                 else
                 {
                     $($("#"+ info.targetId).get(0)).addClass("on1");
                 }
                 }
             
             //check connection of good circle ..            
             if($($("#battery").parent(0).get(0)).hasClass("on2"))
                      {
                          var Goodcircle=true;
                  
                          
                          
                 //  loop on element which has two connection ..      
                 $(".on2").each(function(i,element){
                     
                   
                   if( $("#"+element.id+" img").attr("id") == "wood")
                        Goodcircle=false;
                    else 
                      if( $("#"+element.id+" img").attr("id") == "plastic")
                        Goodcircle=false;
                    
                      // if element has one connection only !  
                      if( $(".on1:not(.on2)").length > 0 )
                        Goodcircle=false;
                          
                      });
                          
                          
                    if(!$($("#switch").parent(0).get(0)).hasClass("on2"))  
                     {     
                         
                      if( Goodcircle)
                     {
                           if( $("#lamp").parent(0).hasClass("on2"))
                            $("#lamp").get(0).src="../Data/Lessons/0/images/lamp%20on.png" ;

                     }
                   else
                     {
                          $("#lamp").get(0).src="../Data/Lessons/0/images/lamp%20off.png" ;
                     }
                      
                     }
                          else
                              {
                                  var sourceImg=$("#switch").get(0).src;
                                  
             
                                        
                               if(sourceImg.substring(sourceImg.indexOf("images") , sourceImg.length) != "images/switch%20off.png")
                             {
                                 if(Goodcircle)
                                    {
                                        if( $("#lamp").parent(0).hasClass("on2"))
                                            $("#lamp").get(0).src="../Data/Lessons/0/images/lamp%20on.png" ;

                                    }
                                 else
                                    {
                                            $("#lamp").get(0).src="../Data/Lessons/0/images/lamp%20off.png" ;
                                    }
                                 
                             }
                                  
                              }
                      }
             
         });
        
             //when drag connection .. prevent self connection
         jsPlumb.bind("beforeDrop", function (info) {
               
                 
               //prevent self connection 
               if (info.sourceId === info.targetId)
                { //source and target ID's are same
                 
                 return false;
             } 
               else           
               { 
                
               //remove past connection 
               // Before new connection is created
             /*
             var src=ci.sourceId;
                var con=jsPlumb.getConnections({source:src}); // Get all source el. connection(s) except the new connection which is being established 
                if(con.length!=0 && $('#'+src).hasClass('exit'))
                    for(var i=0;i<con.length;i++)
                            jsPlumb.detach(con[i]);
                   
                   */
                return true; // true for establishing new connection
               
             
               }
            });
             
             
             // when connection detached ..
          jsPlumb.bind("connectionDetached", function (info) {
  
                    // call DetachedConnectors function ..
                    DetachedConnectors(info);
              
                });
             
           
             // when click question btn change btn from ابدأ  to  إغلاق
            $("#question_btn").click(function(){
                 $(".modal-footer").empty().append('<button type="button" class="btn btn-primary" data-dismiss="modal">إغلاق</button>');
                $("#myModal").modal('show');  
            });
             
             
             $("#switch").dblclick(function(e){
                 
                if($(this).parent(0).hasClass("on2")) 
                 {
                     var Goodcircle=true;
                
            
                 $(".on2").each(function(i,element){
                     
                   
                   if( $("#"+element.id+" img").attr("id") == "wood")
                        Goodcircle=false;
                    else 
                      if( $("#"+element.id+" img").attr("id") == "plastic")
                        Goodcircle=false;
                    
                     
                    
                 })
                
                 if(!$($("#battery").parent(0).get(0)).hasClass("on2"))
                        Goodcircle=false;
                 
                 if( $(".on1:not(.on2)").length > 0 )
                     Goodcircle=false;
                
                    
                 /** turn on / off img .. */
                 var sourceImg=$(this).get(0).src;
                 
                 if(sourceImg.substring(sourceImg.indexOf("images") , sourceImg.length) == "images/switch%20off.png")
                 {
                   $(this).get(0).src="../Data/Lessons/0/images/switch%20on.png";
                 }
                 else
                     {
                          $(this).get(0).src="../Data/Lessons/0/images/switch%20off.png"; 
                          Goodcircle=false;
                     }
               
                 
           
                     
                     if( Goodcircle)
                     {
                         
                        if( $("#lamp").parent(0).hasClass("on2"))
                        {
                            
                        $("#lamp").get(0).src="../Data/Lessons/0/images/lamp%20on.png" ;
                        $(this).get(0).src="../Data/Lessons/0/images/switch%20on.png";
                        }
                     }
                    else
                     {
                      
                         $("#lamp").get(0).src="../Data/Lessons/0/images/lamp%20off.png" 
                     }
        
                 }
             });
           
           //close notification   
         $('.close_msg').click(function() {
        $(".notification_msg").remove();
    });
            
             
              });
 function turnLamp() 
{   
        var Goodcircle=true;
                          
         $(".on2").each(function(i,element){
                     
                   
                   if( $("#"+element.id+" img").attr("id") == "wood")
                        Goodcircle=false;
                    else 
                      if( $("#"+element.id+" img").attr("id") == "plastic")
                        Goodcircle=false;
                    
                      if( $(".on1:not(.on2)").length > 0 )
                     Goodcircle=false;
                          
                      });
                      
         if(!$($("#battery").parent(0)).hasClass("on2"))
                        Goodcircle=false;
        
                      if( Goodcircle)
                     {
                           if( $("#lamp").parent(0).hasClass("on2"))
                               $("#lamp").get(0).src="../Data/Lessons/0/images/lamp%20on.png" ;

                     }
                   else
                     {
                          $("#lamp").get(0).src="../Data/Lessons/0/images/lamp%20off.png" ;
                     }
    }
 
    function HidePoints(element){
        //to hide endpoints
         var eps=jsPlumb.getEndpoints(element);
                for(var j=0;j<eps.length;j++)
                {
                    eps[j].setVisible(false);   // Set visibility of endpoint to false
                }
                    
    }

    function DetachedConnectors(info){
                
                // remove classes on1 or on2 .. 
               if($($("#"+ info.sourceId).get(0)).hasClass("on2"))
                 {
                     $($("#"+ info.sourceId).get(0)).removeClass("on2");
                 }
    
                else
                    if($($("#"+ info.sourceId).get(0)).hasClass("on1"))
                 {
                     $($("#"+ info.sourceId).get(0)).removeClass("on1");
                 }
    
        
                if($($("#"+ info.targetId).get(0)).hasClass("on2"))
                 {
                     $($("#"+ info.targetId).get(0)).removeClass("on2");
                 }
    
                else
                    if($($("#"+ info.targetId).get(0)).hasClass("on1"))
                 {
                     $($("#"+ info.targetId).get(0)).removeClass("on1");
                 }
                 
                 //check lamp to off 
                  turnLamp();
      }
   