$(document).ready(function() {
        

         /* $('.audio_player').acornMediaPlayer({
                tooltipsOn:false
            });*/
        //load lesson json file.
          /*   var lesson_id= sessionStorage.getItem('lessonId');*/
                var lesson_id="G05_T01_U01_CH01_L01";
               sessionStorage.setItem('lessonId',lesson_id);
        var index =0;
         MyObject="";

         var edit ="/lesson_player";

         loadScript("Data/Lessons/"+lesson_id+"/lesson_key.js",function (a, b) {}).onload = function ()
        {
            
           //   MyObject = JSON.parse(lesson_data);
       
        //get pages data ..
         /*   $('.lesson_title').html(sessionStorage.getItem('Title'));*/
        
        
              drawpage(index,lesson_data,lesson_id);
        
        
        //next Page Action ..
            $("#nextLink").click(function(){
                
              
                
             //get Content..
              
                  if(index >= (lesson_data.length-1))
                     { 
                        // $(this).bind('click',function(){      
                         
                                // don't do any thing 
                                return false;                           
                        // });          
                     }
                    else
                    {                  
                        //draw page
                     index++;
                     drawpage(index,lesson_data,lesson_id);
                    }
            });
        
        //Prev Page Action ..
          
        $("#prevLink").click(function(){
                
                index--;       
                //draw page
                drawpage(index,lesson_data,lesson_id);

            });
            
        };
        
        $("#savebookmark_btn").click(function(){
            
            //Add in specific lesson_Id & Page_Id
                //bookmark Name :: bookmark_lessonId_PageId
            
             bookmark = [];
            
            if (localStorage.getItem("bookmark_"+lesson_id+"_"+index))
            {
                
                
                // can't add element in array direct,so I should get array and edit it and add it again .. 
                var pre_storage= localStorage.getItem("bookmark_"+lesson_id+"_"+index);
                var bookmark_array=JSON.parse(pre_storage);
              
                //save in older bookmark ..
                  bookmark_array.push($("#b_mark_words").val()); 
                  localStorage.setItem("bookmark_"+lesson_id+"_"+index, JSON.stringify(bookmark_array));
                    
            }
            else
                {
                    bookmark[0] = $("#b_mark_words").val(); 
                    localStorage.setItem("bookmark_"+lesson_id+"_"+index, JSON.stringify(bookmark));
                  
                }
            
        })
        
        
        $("#saveNotebtn").click(function(){
               
                //Add in specific lesson_Id & Page_Id
                //Note Name :: Note_lessonId_PageId
            
                Note=[];
            
            if(localStorage.getItem("Note_"+lesson_id+"_"+index)) {
                
                
                var pre_storage= localStorage.getItem("Note_"+lesson_id+"_"+index);
                var note_array=JSON.parse(pre_storage);
              
                //save in older bookmark ..
                  note_array.push($("#b_note_words").val()); 
                  localStorage.setItem("Note_"+lesson_id+"_"+index, JSON.stringify(note_array));
                    
            }
            else
                {
                    Note[0] = $("#b_note_words").val(); 
                    localStorage.setItem("Note_"+lesson_id+"_"+index, JSON.stringify(Note));
                  
                }
        })
        
        // View Bookmark & Note ..  
        $("#m_bookmark").click(function(){
            
            //bookmark code here 
            
            ViewBookmark(lesson_id,index);
        
            // Note code here 
            //code here 
                viewNotes(lesson_id,index)
                 });


         
                
        });
        
        function viewNotes(lesson_id,index)
        {
                 $("#ViewNotes").empty();
              var past_storage= localStorage.getItem("Note_"+lesson_id+"_"+index);
             if( past_storage != null)
             {
                 
                 var note_array=JSON.parse(past_storage);
             
          $("#ViewNotes").append('<table class="table">');
                 
             for(i=0;i<note_array.length;i++)
             {
                 $("#ViewNotes table").append("<tr id='"+note_array[i]+"'><td >"+note_array[i]+"</td><td align='left'><input type='button' class='btn btn-info' value='تعديل' onclick='ShowEditNoteForm("+i+","+lesson_id+","+index+")' /></td><td>&nbsp; &nbsp; <input type='button' class='btn btn-danger' value='مسح' onclick='showNoteMsg(\""+note_array[i]+"\","+lesson_id+","+index+")' /></td></tr>")
             }
                 
           
             }
        }
        
       function ViewBookmark(lesson_id,index){
           
           // empty text which I display in ..
            $("#ViewBookmarks").empty();
           
           
             var pre_storage= localStorage.getItem("bookmark_"+lesson_id+"_"+index);
           
             if( pre_storage != null)
             {
                 
                 var bookmark_array=JSON.parse(pre_storage);
             
          $("#ViewBookmarks").append('<table class="table">');
                 
             for(i=0;i<bookmark_array.length;i++)
             {
                 $("#ViewBookmarks table").append("<tr id='"+bookmark_array[i]+"'><td >"+bookmark_array[i]+"</td><td align='left'><input type='button' class='btn btn-info' value='تعديل' onclick='ShowEditMarkForm("+i+","+lesson_id+","+index+")' /></td><td>&nbsp; &nbsp; <input type='button' class='btn btn-danger' value='مسح' onclick='showMarkMsg(\""+bookmark_array[i]+"\","+lesson_id+","+index+")' /></td></tr>")
             }
             }  
       }
        
        function drawpage(index,Page,lesson_id)
        {
            
                endpage = Page.length;
            
                  if(index == 0)
                    {
                            $("#prevLink").css("display","none");
                    }
                    else
                    {
                            $("#prevLink").css("display","block");

                    }
        
                  if(index == (endpage-1))
                     { 
                         $("#nextLink i").removeClass("fa fa-chevron-left");
                     }
                     else
                    {
                             if(!($("#nextLink i").hasClass("fa fa-chevron-left")))
                                    $("#nextLink i").addClass("fa fa-chevron-left");
                 
                    }
                  
            
                $(".footer_nav li span").html("صفحة "+(index+1)+" من "+endpage+"");
            
            
            
            // page content
             Pageheight=parseInt($(window).height())-50;
            
            if(Page[index].type == "html")
            {
  
                //use <object data=""> to load html code and run it . 
                $('.site_contents').html($('<div><object data="pages/'+Page[index].source+'.html" style="width:100%;  min-height:'+Pageheight+'px"></object></div>'));
         
           
                // Save data to sessionStorage
                //check if html has json file
                if(Page[index].jsonSource !="")
                sessionStorage.setItem('jsonFile', Page[index].jsonSource);
                
             

                    
            }
            else if(Page[index].type == "image")                
            {
                $('.site_contents').html($('<div class="img_cont"> <img style="margin:0 auto; width:100%" height="'+Pageheight+'px" src="' + Page[index].source + '"/><div>'));
         
             
            }
            else
            {
                  
                       $('.site_contents').html($('<div class="video_cont"> <video class="main_content" autoplay controls="controls" style="display: block;"><source src="Data/Lessons/'+lesson_id+'/video/'+ Page[index].source + '.ogv" type="video/ogg"/><source src="Data/Lessons/'+lesson_id+'/video/'+ Page[index].source + '.mp4" type="video/mp4"/></video><div>'));
                 
            }

              
        if(Page[index].audio != ''){
            $('.container-fluid .row div:nth-child(1)').empty();
            $('.container-fluid .row div:nth-child(1)').html('<audio class="audio_player" width="100%" height="100%" controls autoplay><source type="audio/ogg" src="'+lesson_data[index].audio+'.ogg"/><source type="audio/ogg" src="'+lesson_data[index].audio+'.mp3"/></audio>');

            $('.audio_player').acornMediaPlayer({
                tooltipsOn:false
            });
    
        }else{
            $('.container-fluid .row div:nth-child(1)').empty();

        }
          
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

     
function DeleteMark(mark_value,lesson_id,index){
        var json = JSON.parse(localStorage["bookmark_"+lesson_id+"_"+index]);
        
        for ( var i = 0; i < json.length; i++ ) 
           {
                 if ( json[i] === mark_value ){
                        json.splice(i,1);
                 }
           }
    
        localStorage["bookmark_"+lesson_id+"_"+index] = JSON.stringify(json);
    
    
    $("tr[id='"+mark_value+"']").fadeOut("slow");
    $("#myDeleteMark").modal('toggle');
}

function DeleteNote(mark_value,lesson_id,index){
        var json = JSON.parse(localStorage["Note_"+lesson_id+"_"+index]);
        
        for ( var i = 0; i < json.length; i++ ) 
           {
                 if ( json[i] === mark_value ){
                        json.splice(i,1);
                 }
           }
    
        localStorage["Note_"+lesson_id+"_"+index] = JSON.stringify(json);
    
    
    $("#ViewNotes tr[id='"+mark_value+"']").fadeOut("slow");
    $("#myDeleteNote").modal('toggle');
}

function showMarkMsg(mark_value , lesson_id ,index){ 
    
  document.getElementById("deletebtn").setAttribute('onclick',"DeleteMark(\""+mark_value+"\","+lesson_id+"," +index+");"); // for FF
 
    $("#deletebtn").onclick = function() {  
                var json = JSON.parse(localStorage["bookmark_"+lesson_id+"_"+index]);
                
                for ( var i = 0; i < json.length; i++ ) 
                   {
                         if ( json[i] === mark_value ) {
                                json.splice(i,1);
                         }
                   }
            
                localStorage["bookmark_"+lesson_id+"_"+index] = JSON.stringify(json);
 }
        
            $("#myDeleteMark").modal("show");
        }

function showNoteMsg(note_value ,lesson_id ,index){ 
    
  document.getElementById("deleteNotebtn").setAttribute('onclick',"DeleteNote(\""+note_value+"\","+lesson_id+"," +index+");"); // for FF
 
    $("#deleteNotebtn").onclick = function() {  
                var json = JSON.parse(localStorage["Note_"+lesson_id+"_"+index]);
                
                for ( var i = 0; i < json.length; i++ ) 
                   {
                         if ( json[i] === note_value ) {
                                json.splice(i,1);
                         }
                   }
            
                localStorage["Note_"+lesson_id+"_"+index] = JSON.stringify(json);
         $("#ViewNotes tr[id='"+mark_value+"']").fadeOut("slow");
    $("#myDeleteNote").modal('toggle');
 }
        
            $("#myDeleteNote").modal("show");
        }

// Edit Mark
function EditMark(i,lesson_id,index){
    
    var mark_value=$("#e_mark_words").val();
    
        var json = JSON.parse(localStorage["bookmark_"+lesson_id+"_"+index]);
        json[i] = mark_value;
        localStorage["bookmark_"+lesson_id+"_"+index] = JSON.stringify(json);
    
        ViewBookmark(lesson_id,index)
    $("#edit_modal").modal('toggle');
    
}

function ShowEditMarkForm(i,lesson_id,index){
     
   
      document.getElementById("editmarkbtn").setAttribute('onclick',"EditMark("+i+","+lesson_id+"," +index+");"); // for FF
 
    $("#editmarkbtn").onclick = function() {  
        
        var json = JSON.parse(localStorage["bookmark_"+lesson_id+"_"+index]);
        json[i] = $("#e_mark_words").val();
        localStorage["bookmark_"+lesson_id+"_"+index] = JSON.stringify(json);
    
        ViewBookmark(lesson_id,index);
          $("#edit_modal").modal('toggle');
 }
    
   $("#edit_modal").modal('show');           
                    
 }



//Edit Note 

function EditNote(i,lesson_id,index){
    
    var mark_value=$("#e_note_words").val();
    
        var json = JSON.parse(localStorage["Note_"+lesson_id+"_"+index]);
        json[i] = mark_value;
        localStorage["Note_"+lesson_id+"_"+index] = JSON.stringify(json);
    
        viewNotes(lesson_id,index);
    $("#edit_Nodemodal").modal('toggle');
    
}

function ShowEditNoteForm(i,lesson_id,index){
     
   
      document.getElementById("editnotebtn").setAttribute('onclick',"EditNote("+i+","+lesson_id+"," +index+");"); // for FF
 
    $("#editnotebtn").onclick = function() {  
        
        var json = JSON.parse(localStorage["Note_"+lesson_id+"_"+index]);
        json[i] = $("#e_note_words").val();
        localStorage["Note_"+lesson_id+"_"+index] = JSON.stringify(json);
        viewNotes(lesson_id,index);
        
          $("#edit_modal").modal('toggle');
 }
    
   $("#edit_Nodemodal").modal('show');           
                 
    
 }