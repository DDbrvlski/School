   M S B A M L   `   `   `  ����A  =Szkola, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SzkolaSzkola  5   	   ;"clr-namespace:Szkola.ViewResourcesSzkola.ViewResources  5   	       Szkola.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Szkola     5   	   3resx"clr-namespace:Szkola.ViewResources     5   	     � Title$  Szkola��5	   	   $	��700��6   $
��1200��6%    � 
ResizeMode$ NoResize=�62    � 	Resources 5
       �� Source�� 5   
   $ MainWindowResources.xaml?�6   %     � Triggers 5      :� 5   
    :� RoutedEvent$ ButtonBase.Click��6    :� 
SourceName$ ButtonOpenMenu��67   .[�5      ��    OpenMenu#��[  6   5      :� 5   
   $ ButtonBase.Click��6   $ ButtonCloseMenu��66   .[�5      ��   	CloseMenu#��[ 6   5       J� InputBindings 5      �� 5   
    �� 	Modifiers$
 Ctrlf�6   
	 �� Key$	 O��6&   
 �� Command
 6.   �� *NoweOgloszenieCommand+�� 5   
   $
 Ctrlf�6   $	 P��6&   
 6.   �� *NowyPlanLekcjiCommand+�� 5   
   $
 Ctrlf�6   $	 K��6&   
 6.   �� *NowaKlasaCommand+�� 5   
   $
 Ctrlf�6   $	 U��6&   
 6.   �� *NowyUzytkownikCommand+�� 5   
   $ Ctrl+Altf�6   $	 O��6*   
 62   �� *OgloszeniaCommand+�� 5   
   $ Ctrl+Altf�6   $	 P��6*   
 62   �� *PlanyLekcjiCommand+�� 5   
   $ Ctrl+Altf�6   $	 K��6*   
 62   �� *KlasyCommand+�� 5   
   $ Ctrl+Altf�6   $	 U��6*   
 62   �� *UzytkownicyCommand+�� 5   
   $	 Altf�6   $	 F4��6%   
 6.   �� *ZamknijCommand+.��5       � -   5         �Name$ GridMain��5         GridBackground#��[ 6    � RowDefinitions 5!   
   �� 5"      $��25 �6   �� 5#      $��38 �6   �� 5$      $��* �6   �� 5%      $��30 �6   .+�5(   
   o�   Menu#��[ 6   .@�5)      m� *   $Szkola.ViewResources.GlobalResources   Plik#��Z 6   .@�5*      m�    Zamknij#��Z 6    m� InputGestureText$ Alt+F4��6o    m� Command 6L   �� *ZamknijCommand+6o   5+      m� 5,         
Uzytkownik#��Z 6   .@�5-      m�    DodajUzytkownika#��Z 6   $ Ctrl+U��6    6U   �� *NowyUzytkownikCommand+ m� Icon 5.      �� 5/      $4��./View/Content/Images/DodajUzytkownikaImage.png��6    51      m� 52         WszyscyUzytkownicy#��Z 6   $ 
Ctrl+Alt+U��6~    6W   �� *UzytkownicyCommand+ 53      �� 54      $/��)/View/Content/Images/UzytkownicyImage.png��6    56      57      m� 58         Klasa#��Z 6   .@�59      m�    
DodajKlase#��Z 6   $ Ctrl+K��6t    6O   �� *NowaKlasaCommand+ 5:      �� 5;      $.��(/View/Content/Images/DodajKlaseImage.png��6    5=      m� 5>         WszystkieKlasy#��Z 6   $ 
Ctrl+Alt+K��6t    6S   �� *KlasyCommand+ 5?      �� 5@      $)��#/View/Content/Images/KlasyImage.png��6    5B      5C      m� 5D         
Ogloszenia#��Z 6   .@�5E      m�    DodajOgloszenie#��Z 6   $ Ctrl+O��6~    6T   �� *NoweOgloszenieCommand+ 5F      �� 5G      $3��-/View/Content/Images/DodajOgloszenieImage.png��6    5I      m� 5J         WszystkieOgloszenia#��Z 6   $ 
Ctrl+Alt+O��6~    6X   �� *OgloszeniaCommand+ 5K      �� 5L      $.��(/View/Content/Images/OgloszeniaImage.png��6    5N      5O      m� 5P         
PlanLekcji#��Z 6   .@�5Q      m�    DodajPlanLekcji#��Z 6   $ Ctrl+P��6~    6T   �� *NowyPlanLekcjiCommand+ 5R      �� 5S      $3��-/View/Content/Images/DodajPlanLekcjiImage.png��6    5U      m� 5V         WszystkiePlanyLekcji#��Z 6   $ 
Ctrl+Alt+P��6�    6Y   �� *PlanyLekcjiCommand+ 5W      �� 5X      $/��)/View/Content/Images/PlanyLekcjiImage.png��6    5Z      5[      5\      c� 5^   
   $��1��6     ToolBar#��[ 6   .@�5_      ��   ButtonTopPanel#��[ 6    � ToolTip$ Wyszukaj użytkowników��6=   ��6]   �� *UzytkownicyCommand+.��5`      �� $/��)/View/Content/Images/UzytkownicyImage.png��6     ImageTopBar#��[ 6K   5a      �� 5b      #��[ 6   $ Dodaj nowego użytkownika��6=   ��6`   �� *NowyUzytkownikCommand+.��5c      �� $4��./View/Content/Images/DodajUzytkownikaImage.png��6   #��[ 6P   5d      �� 5e        SeparatorStandard#��[ 6   �� 5f      #��[ 6   $ Wyszukaj klasy��6=   ��6W   �� *KlasyCommand+.��5g      �� #��[ 6   $)��#/View/Content/Images/KlasyImage.png��6=   5h      �� 5i      #��[ 6   $ Dodaj nową klasę��6=   ��6Y   �� *NowaKlasaCommand+.��5j      �� #��[ 6   $.��(/View/Content/Images/DodajKlaseImage.png��6=   5k      �� 5l      #��[ 6   �� 5m      #��[ 6   $ Wyszukaj plan lekcji��6=   ��6]   �� *PlanyLekcjiCommand+.��5n      �� $/��)/View/Content/Images/PlanyLekcjiImage.png��6   #��[ 6K   5o      �� 5p      #��[ 6   $ Dodaj nowy plan lekcji��6=   ��6_   �� *NowyPlanLekcjiCommand+.��5q      �� $3��-/View/Content/Images/DodajPlanLekcjiImage.png��6   #��[ 6O   5r      �� 5s      #��[ 6   �� 5t      #��[ 6   $ Wyszukaj ogłoszenia��6=   ��6\   �� *OgloszeniaCommand+.��5u      �� $.��(/View/Content/Images/OgloszeniaImage.png��6   #��[ 6J   5v      �� 5w      #��[ 6   $ Dodaj nowe ogłoszenie��6=   ��6^   �� *NoweOgloszenieCommand+.��5x      �� $3��-/View/Content/Images/DodajOgloszenieImage.png��6   #��[ 6O   5y      5z      � 5|   
   $��2��6     GridMenuBackground#��[ 6    � ColumnDefinitions 5}      �� 5~      .+�5�      � -   5        $ GridMenu��5�      $	��220��6%    	 GridLeftMenu#��[	 61    5�      �� 5�      $��38 �6$   �� 5�      $��* �6$   .+�5�      � 
�������$��40��6*   $��Bottom=�66   .+�5�      ��  
 StackPanelMenuBoczne#��[
 6!   .+�5�      �� -   5          ��Name$  ButtonCloseMenu��5�   !     ButtonMenuBoczne#��[ 6X   .��5�      �� $-��'/View/Content/Images/ArrowBackImage.png��6$     ImageMenuBoczne#��[ 6U   5�      �� 5�      -   5        $  ButtonOpenMenu��5�   !   $y�	Collapsed=�69   #��[ 6m   .��5�      �� $(��"/View/Content/Images/MenuImage.png��6$   #��[ 6P   5�      5�      5�      � 5�      $��2��6   $	��240��6$   $��-6 0 0 0q�60   .+�5�      �� $��0��6)   $��1��5�   )   $��0 -2 0 0q�5�   )   $��0q�5�   )     CommandsTemplate#��[ 5�   )     MainHCCStyle#��[ 5�   )   ��5�   )   �� ! �� Path$! Commands�5�   )   5�      5�      �� 5�      -   5        " ��Name$" TabMenu��5�   )   $��2��5�   )     WorkspacesTemplate#��[ 5�   )   #��[ 5�   )   $��Right=�5�   )   $	��960��5�   )   ��5�   )   �� $! 
Workspaces�5�   )   5�      �� 5�   
   $��3��6   .@�5�      ��   StatusBarItemLeft#��[ 6   .��6G   Szkola6O   �� 5�        StatusBarItemRight#��[ 6   .��6H   
Zakladka6R   5�      5�      5�      