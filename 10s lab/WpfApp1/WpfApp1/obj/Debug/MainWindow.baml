   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ TList��5      $ �� IsSynchronizedWithCurrentItem$
 TrueY�6R   ��6#   �� *	ItemLists+ �� Mode$ TwoWay=���5      �� 5      .R�5      �� $��10q�6%   .+�5      �� $
��Bold"�6?   $��18(�6Q   ��6(   �� *Name+6Q   �� 5      ��6(   �� *Cost+�� 5      ��6(   �� *	Article+�� 5       �� Orientation$ 
Horizontal=�6)   .+�5   "   �� $��Осталось:��6,   �� 5   "   $
��Bold"�6O   $
��10,0q�6a   ��6,   �� *Count+$ TwoWay=�6a   �� 5   "   $
��шт��6,   5   #   5   #   5      5      � 5   
   $��1��6   $��0,0,1,0q�6   .+�5       �� 
�������$��1q�62   $��10q�6F   ._�5!      �� $��10q�6%   .+�5"      �� $��Имя:��6(   �� 5#      -   5        $
��Name��5#   &   $��0,0,0,10q�6m   ��62   �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=�6m   �� 5%      $��Артикль:��6(   �� 5&      -   5        $��Article��5&   &   $��0,0,0,10q�6s   ��65   �� *	Article+$ PropertyChanged=�6s   �� 5(      $��	Цена:��6(   �� 5)      -   5        $
��Cost��5)   &   $��0,0,0,10q�6m   ��62   �� *Cost+$ PropertyChanged=�6m   �� 5+      $ 
Horizontal=�6*   .+�5,   "   �� -   5        $��UP��5,   )   $��Обновление��63   $
��10,0q�6n   ��6H   �� *GiveItemCommand+6|   �� 5-   "   -   5        $��Minus��5-   )   $��-��67   $
��10,0q�6f   ��6C   �� *GetItemCommand+6t   �� 5.   "   -   5        $
��Plus��5.   )   $��+��66   $
��10,0q�6f   ��6B   �� *AddItemCommand+6t   50      51      52      53      54      55      