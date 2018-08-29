//All general attributes can be commented or deleted
//•	We have seven types of tabs.
//1.	Text
//2.	image
//3.	textWithVideo
//4.	TextWithSlider
//5.	Slider
//6.	Video
//7.	Image
//•	Any dynamicData attribute must be written in code
//•	Template can run OGG or MP3 audio
//•	Template can run OGV or MP4 video
//•	Write audio or video source without extensions.
var mydata={
"staticData":{
    "pageHeader":"الغيوم",//Template title appears on Header area
    "prompt":"اضغط على كل عنوان لتعرض الشرح الخاص به.",//Blue prompt which appears at page bottom
    "title":'الغيوم',    
    "inside_title":'<img style="display: table;margin: 0 auto;width: 400px;" src="../Data/lessons/Sci/G07_T02_U04_Ch07_L01/images/G07_T02_U04_Ch07_L01_P10.png">',//Text can be written under header above tabs
    "narration":'',//Audio starts on page load
},
"dynamicData":[  
    {
        "type":"text",
        "title":"الغيوم المنخفضة",
          'sub_narration':'../Data/lessons/Sci/G07_T02_U04_Ch07_L01/audio/G07_T02_U04_Ch07_L01_P10_Tab1',
 "mediaSource":{"src":"../Data/lessons/Sci/G09_T02_U05_Ch12_L03test/images/summery.png","time":"500"},
        "paragraph":[{"text":"<p class='animated bounceInDown'>تتكوّن على ارتفاع 2000 م أو أقل من سطح الأرض. <span style='color:red'>ومن أمثلتها: </span>الغيوم الركامية؛ وهي غيوم سميكة تتشكّل عندما ترتفع تيارات هوائية رطبة إلى أعلى. وتدلّ الغيوم الركامية أحيانًا على طقس معتدل. ولكن عندما يزداد سُمكها تُنتج أمطارًا غزيرة يصاحبها برق ورعد </p>","time":"1000"},],
    },//Type image source    
     
    
    
    {
        "type":"text",
        "title":" الغيوم المتوسطة", 
         'sub_narration':'../Data/lessons/Sci/G07_T02_U04_Ch07_L01/audio/G07_T02_U04_Ch07_L01_P10_Tab1',
        "mediaSource":{"src":"../Data/lessons/Sci/G09_T02_U05_Ch12_L03test/images/summery.png","time":"500"},
        "paragraph":[{"text":"<p  class='animated bounceInDown'>تكون على ارتفاعات تتراوح بين 2000 م - 8000 م، وتتكون من خليط من ماء سائل وبلورات جليدية، وقد تسبب أمطارًا خفيفة. <span style='color:red'><span style='color:red'><span style='color:red'>ومن أمثلتها:</span></span></span> الغيوم الركامية المتوسطة، والغيوم الطبقية المتوسطة.</p> ","time":"1000"},],
    },//Type image source    
    
        {
        "type":"text",
        "title":" الغيوم المرتفعة",  
     'sub_narration':'../Data/lessons/Sci/G07_T02_U04_Ch07_L01/audio/G07_T02_U04_Ch07_L01_P10_Tab1',
        "paragraph":[
            {"text":"<p  class='animated bounceInDown'>تتكوَّن من بلورات جليدية بسبب وجودها على ارتفاعات كبيرة. <span style='color:red'><span style='color:red'>ومن أمثلتها:</span></span> الغيوم الريشية، والغيوم الريشية الركامية، والغيوم الريشية الطبقية. ومن الغيوم نوع آخر يمتدّ عموديًّا على جميع الارتفاعات، ويسمى غيوم المزن الركامية، وتسبب أمطارًا غزيرة وزخّات من الثلج، وقد تولّد عواصف رعدية.</p> ","time":"1000"},
],   
         "mediaSource":""
    }
    
   
    
    ]   
};